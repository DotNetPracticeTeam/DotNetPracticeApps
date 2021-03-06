//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Pubs.Data
{
    public partial class author
    {
        #region Primitive Properties
    
        public virtual string id
        {
            get;
            set;
        }
    
        public virtual string lastname
        {
            get;
            set;
        }
    
        public virtual string firstname
        {
            get;
            set;
        }
    
        public virtual string phone
        {
            get;
            set;
        }
    
        public virtual string address
        {
            get;
            set;
        }
    
        public virtual string city
        {
            get;
            set;
        }
    
        public virtual string state
        {
            get;
            set;
        }
    
        public virtual string zip
        {
            get;
            set;
        }
    
        public virtual bool contract
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual ICollection<titleauthor> titleauthors
        {
            get
            {
                if (_titleauthors == null)
                {
                    var newCollection = new FixupCollection<titleauthor>();
                    newCollection.CollectionChanged += Fixuptitleauthors;
                    _titleauthors = newCollection;
                }
                return _titleauthors;
            }
            set
            {
                if (!ReferenceEquals(_titleauthors, value))
                {
                    var previousValue = _titleauthors as FixupCollection<titleauthor>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= Fixuptitleauthors;
                    }
                    _titleauthors = value;
                    var newValue = value as FixupCollection<titleauthor>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += Fixuptitleauthors;
                    }
                }
            }
        }
        private ICollection<titleauthor> _titleauthors;

        #endregion
        #region Association Fixup
    
        private void Fixuptitleauthors(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (titleauthor item in e.NewItems)
                {
                    item.author = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (titleauthor item in e.OldItems)
                {
                    if (ReferenceEquals(item.author, this))
                    {
                        item.author = null;
                    }
                }
            }
        }

        #endregion
    }
}
