using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SimpleDatabase.Program;

namespace SimpleDatabase
{
    public class Database
    {
        
        Dictionary<string, int> db;

        Database transaction;
        IOutputWriter _writer;

        /// <summary>
        /// Database Constructor, initialized db Dictionary.
        /// </summary>
        public Database(IOutputWriter writer)
        {
            _writer = writer;
            db = new Dictionary<string, int>();
        }

        /// <summary>
        /// Set method. Stores the value in the database at the given key.
        /// </summary>
        /// <param name="key">The key of the new value</param>
        /// <param name="value">The new value to store</param>
        public void Set(string key, int value)
        {
            if (transaction != null)
            {
                transaction.Set(key, value);
            }

            else
            {
                if (!db.ContainsKey(key))
                {
                    db.Add(key, value);
                }

                else
                {
                    db[key] = value;
                }
            }
        }

        /// <summary>
        /// Gets the value of the given key.
        /// </summary>
        /// <param name="key"></param>
        public void Get(string key)
        {
            if (transaction != null)
            {
                transaction.Get(key);
            }
            else
            {
                if (db.ContainsKey(key))
                {
                    _writer.WriteLine(db[key].ToString());
                }
                else
                {
                    _writer.WriteLine("NULL");
                }
            }
        }

        

        /// <summary>
        /// Removes the given key and value from the database if it exists.
        /// </summary>
        /// <param name="key">The key to unset</param>
        public void Unset(string key)
        {
            if (transaction != null)
            {
                transaction.Unset(key);
            }

            else
            {
                if (db.ContainsKey(key))
                {
                    db.Remove(key);
                }
            }
        }

        /// <summary>
        /// Prints out the number of occurences of the given value in the database.
        /// </summary>
        /// <param name="value">The value to count occurence of</param>
        public void NumEqualTo(int value)
        {
            if (transaction != null)
            {
                transaction.NumEqualTo(value);
            }
            else
            {
                _writer.WriteLine(db.Where(kvp => kvp.Value.Equals(value)).Count().ToString());
            }
            
        }

        /// <summary>
        /// Creates a temporary database.
        /// </summary>
        public void Begin()
        {
            if (transaction != null)
            {
                transaction.Begin();
            }

            else
            {
                transaction = new Database(_writer);
                transaction.db = new Dictionary<string, int>(this.db);
            }
        }

        /// <summary>
        /// Rollback most recent transaction. If no current transaction, print NO TRANSACTION
        /// </summary>
        public void Rollback()
        {
            if (transaction == null)
            {
                _writer.WriteLine("NO TRANSACTION");
            }

            else
            {
                rollbackRecursive();
            }

        }

        /// <summary>
        /// Helper function for Rollback
        /// </summary>
        private void rollbackRecursive()
        {
            if (transaction.transaction == null)
            {
                transaction = null;
            }
            else
            {
                transaction.rollbackRecursive();
            }
        }

        public void Commit()
        {
            if (transaction == null)
            {
                _writer.WriteLine("NO TRANSACTION");
            }

            else
            {
                Database current = transaction;
                while (current != null)
                {
                    foreach (string key in current.db.Keys)
                    {
                        if (db.ContainsKey(key))
                        {
                            db[key] = current.db[key];
                        }
                        else
                        {
                            db.Add(key, current.db[key]);
                        }
                    }
                    current = current.transaction;
                }
            }
            transaction = null;
        }

    }
}
