﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CSharpViaTest.Collections._10_EnumerablePractices
{
    /* 
     * Description
     * ===========
     * 
     * This test is the basic practice to manually create a IEnumerator<T>. This can
     * help you to understand the defered nature of IEnumerable<T>, thus helps you 
     * better understanding various query libraries such as LINQ.
     * 
     * Difficulty: Super Easy
     * 
     * Knowledge Point
     * ===============
     * 
     * - An IEnumerator<T> does not necessary load all data into memory. It is just a
     *   simple iterator for the most of the time.
     * - GetEnumerator() just returns the IEnumerator<T> without actually iterating
     *   over the sequence.
     * 
     * Requirement
     * ===========
     * 
     * - No LINQ method is allowed to use in this test.
     * - The memory efficiency should be O(1).
     */
    public class SkippedEnumeratorPractice
    {
        class SkippedEnumerable<T> : IEnumerable<T>
        {
            readonly ICollection<T> collection;

            public SkippedEnumerable(ICollection<T> collection)
            {
                this.collection = collection ?? throw new ArgumentNullException(nameof(collection));
            }

            public IEnumerator<T> GetEnumerator()
            {
                return new SkippedEnumerator<T>(collection);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        #region Please modifies the code to pass the test
	class SkippedEnumerator<T> : IEnumerator<T>
        {
            readonly IEnumerator<T> enumerator;

            public SkippedEnumerator(IEnumerable<T> collection)
            {
                enumerator = collection.GetEnumerator();
            }

            public bool MoveNext()
            {
                return enumerator.MoveNext() && enumerator.MoveNext();
            }

            public void Reset()
            {
                enumerator.Reset();
            }

            public T Current => enumerator.Current;

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                enumerator.Dispose();
            }
        }
        #endregion

        [Fact]
        public void should_visit_elements_in_skipped_manner()
        {
            int[] sequence = {1, 2, 3, 4, 5, 6};
            int[] resolved = new SkippedEnumerable<int>(sequence).ToArray();

            Assert.Equal(new [] {2, 4, 6}, resolved);
        }
    }
}