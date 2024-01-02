using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace IdleGame.Utilities
{
    [Serializable]
    public class Pair<K, V>
    {
        [SerializeField] private K key;
        [SerializeField] private V value;

        public Pair(K key, V value)
        {
            this.key = key;
            this.value = value;
        }

        public K Key => key;

        public V Value
        {
            get => value;
            set => this.value = value;
        }

        public override bool Equals(object obj)
        {
            return obj is Pair<K, V> pair && Equals(pair);
        }

        public bool Equals(Pair<K, V> pair)
        {
            if (pair == null)
                return false;

            return Key.Equals(pair.Key) && Value.Equals(pair.Value);
        }
    }

    [Serializable]
    public class PairsList<K, V> : IList<Pair<K, V>>
    {
        public List<Pair<K, V>> list = new List<Pair<K, V>>();

        public Dictionary<K, V> ToDict() => list.ToDictionary(pair => pair.Key, pair => pair.Value);

        public V this[K key]
        {
            get
            {
                var listElem = list.FirstOrDefault(pair => pair.Key.Equals(key));
                if (listElem == null) return default;
                return listElem.Value ?? default;
            }
            set
            {
                var listElem = list.FirstOrDefault(pair => pair.Key.Equals(key));
                if (listElem == null) return;
                listElem.Value = value;
            }
        }

        public void Add(K key, V value) => Add(new Pair<K, V>(key, value));
        public void Remove(K key, V value) => Remove(new Pair<K, V>(key, value));
        public void Remove(K key) => Remove(key, this[key]);
        public IEnumerator<Pair<K, V>> GetEnumerator() => list.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Add(Pair<K, V> item) => list.Add(item);

        public void Clear() => list.Clear();

        public bool Contains(Pair<K, V> item) => list.Contains(item);

        public void CopyTo(Pair<K, V>[] array, int arrayIndex) => list.CopyTo(array, arrayIndex);

        public bool Remove(Pair<K, V> item) => list.Remove(item);

        public int Count => list.Count;
        public bool IsReadOnly => /*list.IsReadOnly*/ false;
        public int IndexOf(Pair<K, V> item) => list.IndexOf(item);

        public void Insert(int index, Pair<K, V> item) => list.Insert(index, item);

        public void RemoveAt(int index) => list.RemoveAt(index);

        public Pair<K, V> this[int index]
        {
            get => list[index];
            set => list[index] = value;
        }
    }
}