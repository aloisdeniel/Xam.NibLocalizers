namespace NibLocalizers
{
	using Foundation;

	public class BundleLocalizer : Localizer
	{
		public BundleLocalizer()
		{
			this.Bundle = NSBundle.MainBundle;
		}

		public NSBundle Bundle { get; set; }

		protected override string Localize(string key) => Bundle.LocalizedString(key, "");
	}
}
