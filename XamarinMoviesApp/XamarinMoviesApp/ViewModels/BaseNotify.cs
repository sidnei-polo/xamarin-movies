using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using XamarinMoviesApp.ViewModels;

namespace XamarinMoviesApp.ViewModels
{
    public class BaseNotify : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void Dispose()
        {
            ClearEvents();
        }

        internal bool SetPropertyChanged<T>(ref T currentValue, T newValue, [CallerMemberName] string propertyName = "")
        {
            return PropertyChanged.SetProperty(this, ref currentValue, newValue, propertyName);
        }

        internal void SetPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ClearEvents()
        {
            if (PropertyChanged == null)
                return;

            var invocation = PropertyChanged.GetInvocationList();
            foreach (var p in invocation)
                PropertyChanged -= (PropertyChangedEventHandler)p;
        }
    }

    public interface IDirty
    {
        bool IsDirty
        {
            get;
            set;
        }
    }
}

namespace System.ComponentModel
{
    public static class BaseNotify
    {
        public static bool SetProperty<T>(this PropertyChangedEventHandler handler, object sender, ref T currentValue, T newValue, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(currentValue, newValue))
                return false;

            currentValue = newValue;

            var dirty = sender as IDirty;

            if (dirty != null)
                dirty.IsDirty = true;

            if (handler == null)
                return true;

            handler.Invoke(sender, new PropertyChangedEventArgs(propertyName));
            return true;
        }
    }
}
