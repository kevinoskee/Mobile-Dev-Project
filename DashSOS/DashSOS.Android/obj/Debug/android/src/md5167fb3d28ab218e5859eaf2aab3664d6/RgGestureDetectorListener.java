package md5167fb3d28ab218e5859eaf2aab3664d6;


public class RgGestureDetectorListener
	extends android.view.GestureDetector.SimpleOnGestureListener
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onSingleTapUp:(Landroid/view/MotionEvent;)Z:GetOnSingleTapUp_Landroid_view_MotionEvent_Handler\n" +
			"";
		mono.android.Runtime.register ("Rg.Plugins.Popup.Droid.Gestures.RgGestureDetectorListener, Rg.Plugins.Popup.Droid, Version=1.1.4.168, Culture=neutral, PublicKeyToken=null", RgGestureDetectorListener.class, __md_methods);
	}


	public RgGestureDetectorListener ()
	{
		super ();
		if (getClass () == RgGestureDetectorListener.class)
			mono.android.TypeManager.Activate ("Rg.Plugins.Popup.Droid.Gestures.RgGestureDetectorListener, Rg.Plugins.Popup.Droid, Version=1.1.4.168, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public boolean onSingleTapUp (android.view.MotionEvent p0)
	{
		return n_onSingleTapUp (p0);
	}

	private native boolean n_onSingleTapUp (android.view.MotionEvent p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}