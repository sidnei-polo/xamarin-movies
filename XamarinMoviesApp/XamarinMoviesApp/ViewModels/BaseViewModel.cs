using System;
namespace XamarinMoviesApp.ViewModels
{
	public class BaseViewModel : BaseNotify
	{
		bool _isBusy = false;
		public bool IsBusy
		{
			get
			{
				return _isBusy;
			}
			set
			{
				SetPropertyChanged(ref _isBusy, value);
			}
		}

		public virtual void NotifyPropertiesChanged()
		{
		}
	}
}