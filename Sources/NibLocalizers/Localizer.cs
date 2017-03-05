namespace NibLocalizers
{
	using Foundation;
	using UIKit;
	using System.Linq;
	using System.Runtime.CompilerServices;

	public abstract class Localizer : ILocalizer
	{
		protected abstract string Localize(string key);

		public void Localize(NSObject o)
		{
			switch (o)
			{
				case UIBarItem b: Localize(b); break;
				case UIButton b: Localize(b); break;
				case UILabel b: Localize(b); break;
				case UINavigationItem b: Localize(b); break;
				case UISearchBar b: Localize(b); break;
				case UISegmentedControl b: Localize(b); break;
				case UITextField b: Localize(b); break;
				case UITextView b: Localize(b); break;
				case UIViewController b: Localize(b); break;
			}

			var view = o as UIView;
			if(view != null && !(o is UITableView) && view.IsAccessibilityElement)
			{
				view.AccessibilityLabel = Localize(view.AccessibilityLabel);
				view.AccessibilityHint = Localize(view.AccessibilityHint);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void Localize(UIBarItem bi)
		{
			bi.Title = Localize(bi.Title);

			var bbi = bi as UIBarButtonItem;
			if(bbi != null)
			{
				var localized = bbi.PossibleTitles.ToList().Select(x => Localize(x?.ToString()));
				bbi.PossibleTitles = new NSSet(localized);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void Localize(UIButton b)
		{
			var titles = new[]
			{
				b.Title(UIControlState.Normal),
				b.Title(UIControlState.Highlighted),
				b.Title(UIControlState.Disabled),
				b.Title(UIControlState.Selected),
			};

			b.SetTitle(Localize(titles[0]), UIControlState.Normal);
			if(titles[1] == b.Title(UIControlState.Highlighted))
				b.SetTitle(Localize(titles[1]), UIControlState.Highlighted);
			if (titles[2] == b.Title(UIControlState.Disabled))
				b.SetTitle(Localize(titles[2]), UIControlState.Disabled);
			if (titles[3] == b.Title(UIControlState.Selected))
				b.SetTitle(Localize(titles[3]), UIControlState.Selected);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void Localize(UILabel l)
		{
			l.Text = Localize(l.Text);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void Localize(UINavigationItem item)
		{
			item.Title = Localize(item.Title);
			item.Prompt = Localize(item.Prompt);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void Localize(UISearchBar bar)
		{
			bar.Placeholder = Localize(bar.Placeholder);
			bar.Prompt = Localize(bar.Prompt);
			bar.Text = Localize(bar.Text);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void Localize(UISegmentedControl segmented)
		{
			for (int i = 0; i < segmented.NumberOfSegments; i++)
			{
				var title = segmented.TitleAt(i);
				segmented.SetTitle(Localize(title), i);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void Localize(UITextField field)
		{
			field.Text = Localize(field.Text);
			field.Placeholder = Localize(field.Placeholder);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void Localize(UITextView field)
		{
			field.Text = Localize(field.Text);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void Localize(UIViewController vc)
		{
			vc.Title = Localize(vc.Title);
		}
	}
}
