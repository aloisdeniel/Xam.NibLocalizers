namespace NibLocalizers
{
	using System.Globalization;
	using System.Resources;

	public class ResxLocalizer : Localizer
	{
		public ResxLocalizer(ResourceManager manager)
		{
			this.ResourceManager = manager;
		}

		public ResourceManager ResourceManager { get; set; }

		public CultureInfo Culture { get; set; }

		protected override string Localize(string key) => this.ResourceManager.GetString(key, this.Culture);
	}
}
