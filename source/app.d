import gtk.Application : Application;
import gio.Application : GioApplication = Application;
import gtk.ApplicationWindow : ApplicationWindow;
import gtkc.giotypes : GApplicationFlags;
import gtkc.gtktypes : GtkStateType, GdkWindowState, GdkEventWindowState;

import gdk.Color;
import gtk.Main;
import gtk.StatusIcon;
import gtk.Menu;
import gtk.MenuItem;
import gtk.Widget;
import gdk.Event;
import gtk.Label;
import gtk.VBox;
import gtk.HBox;
import gtk.Button;
import gtk.Image;
import gtk.IconSize;
import pango.PgFontDescription;
import gdk.Pixbuf;
import gtk.Alignment;

public int main (string [] args)
{
	auto application = new Application ("rs.myst.deezcord", GApplicationFlags.FLAGS_NONE);
	application.addOnActivate (delegate void (GioApplication) { new MainWindow (application); });
	return application.run (args);
}

private class MainWindow : ApplicationWindow
{
	public this (Application application)
	{
		super (application);
		initUI ();
		showAll ();
	}

	private void initUI ()
	{
		setSizeRequest (320, 480);
		setTitle ("deezcord");
		setWmclass ("deezcord", "deezcord");
		setIconFromFile ("assets/small_icon.png");
		setResizable (false);
		Color bg;
		Color.parse ("#2C2F33", bg);
		modifyBg (GtkStateType.NORMAL, bg);
		Color fg;
		Color.parse ("#FFFFFF", fg);
		modifyFg (GtkStateType.NORMAL, fg);

		StatusIcon statusIcon = new StatusIcon ("assets/small_icon.png", true);
		statusIcon.setName ("deezcord");
		statusIcon.setTooltipText ("deezcord");

		statusIcon.addOnActivate (delegate void (StatusIcon)
		{
			this.show ();
			this.deiconify ();
		});

		Menu menu = new Menu ();
		MenuItem menuItemExit = new MenuItem ("Exit");
		menuItemExit.addOnActivate (delegate void (MenuItem)
		{
			import core.stdc.stdlib : exit;
			exit (0);
		});

		statusIcon.addOnPopupMenu (delegate void (uint button, uint activateTime, StatusIcon)
		{
			menu.popup (button, activateTime);
		});

		menu.append (menuItemExit);
		menu.showAll ();

		statusIcon.setVisible (false);

		addOnWindowState (delegate bool (GdkEventWindowState* event, Widget widget)
		{
			if (event.changedMask & GdkWindowState.ICONIFIED)
			{
				if (event.newWindowState & GdkWindowState.ICONIFIED)
				{
					widget.hide ();
					statusIcon.setVisible (true);
				}
				else
				{
					statusIcon.setVisible (false);
				}
			}

			return true;
		});

		VBox mainBox = new VBox (false, 0);
		add (mainBox);

		Pixbuf pb = new Pixbuf ("assets/small_icon.png", 50, 50);

		Image icon = new Image (pb);
		icon.setMarginTop (5);

		PgFontDescription font = new PgFontDescription ("Muli", 22);

		Label appName = new Label ("<b>deezcord</b>");
		appName.setUseMarkup (true);
		appName.modifyFont (font);
		
		HBox h = new HBox (false, 10);
		h.add (icon);
		h.add (appName);
		h.setHalign (GtkAlign.CENTER);
		h.setValign (GtkAlign.START);
		h.setMarginTop (50);

		mainBox.add (h);
	}
}
