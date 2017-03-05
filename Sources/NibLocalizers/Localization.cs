namespace NibLocalizers
{
	using System;
	using System.Resources;
	using System.Runtime.InteropServices;
	using Foundation;
	using ObjCRuntime;


	public class Localization
	{
		#region Swizzling

		const string LibObjC = "/usr/lib/libobjc.dylib";

		[DllImport(LibObjC)]
		extern static IntPtr class_getInstanceMethod(IntPtr classHandle, IntPtr Selector);

		[DllImport(LibObjC)]
		extern static IntPtr imp_implementationWithBlock(ref BlockLiteral block);

		[DllImport(LibObjC)]
		extern static void method_setImplementation(IntPtr method, IntPtr imp);

		static Localization()
		{
			var method = class_getInstanceMethod(new NSObject().ClassHandle, new Selector("awakeFromNib").Handle);
			var block_value = new BlockLiteral();
			CaptureDelegate d = AwakeFromNibCapture;
			block_value.SetupBlock(d, null);
			var imp = imp_implementationWithBlock(ref block_value);
			method_setImplementation(method, imp);
		}

		delegate void CaptureDelegate(IntPtr block, IntPtr self, IntPtr uiView);

		[MonoPInvokeCallback(typeof(CaptureDelegate))]
		static void AwakeFromNibCapture(IntPtr block, IntPtr self, IntPtr uiView) => Current.Localize(Runtime.GetNSObject(self));

		#endregion

		#region Initialization

		private static ILocalizer localizer;

		private static ILocalizer Current => localizer ?? throw new InvalidOperationException("The Localizer has to be initilized first");

		public static BundleLocalizer InitializeFromBundle()  => (BundleLocalizer)(localizer = new BundleLocalizer());

		public static ResxLocalizer InitializeFromResx(ResourceManager resx) => (ResxLocalizer)(localizer = new ResxLocalizer(resx));

		public static ILocalizer Initialize(ILocalizer localizer) => Localization.localizer = localizer;

		#endregion
	}
}
