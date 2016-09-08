using System;
using Gtk;
using System.Net;
using System.IO;

public partial class MainWindow: Gtk.Window
{
	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();
		estado ();
		String version = "Versión: 1.0.1";
		label7.Text = version;
		label8.Text = version;
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void recibir (object sender, EventArgs e)
	{
		String token = entry3.Text;
		try{
			String content = api("http://viset.vivarsoft.es/archivos/"+ token +".txt");
			textview4.Buffer.Text = content;
		}
		catch(WebException)
		{
			textview4.Buffer.Text = "Error";
		}
	}

	protected void enviar (object sender, EventArgs e)
	{
		String texto = textview2.Buffer.Text;
		try{
			String content2 = api("http://viset.vivarsoft.es/viset-mobile.php?msg="+ texto);
			content2  = content2.Replace("<br>", " \n ");
			textview3.Buffer.Text = content2;
		}
		catch(WebException)
		{
			textview3.Buffer.Text = "Error";
		}
	}

	private string api(String url)
	{
		WebClient client = new WebClient();
		Stream stream = client.OpenRead(url);
		StreamReader reader = new StreamReader(stream);
		String content2 = reader.ReadToEnd();
		return content2;
	}

	private void estado ()
	{
		HttpWebRequest req = WebRequest.Create(
			"http://viset.vivarsoft.es/") as HttpWebRequest;
		HttpWebResponse rsp;
		try {
			rsp = req.GetResponse() as HttpWebResponse;
		} catch (WebException e) {
			if (e.Response is HttpWebResponse) {
				rsp = e.Response as HttpWebResponse;
			} else {
				rsp = null;
			}
		}
		if (rsp != null) {
			if (rsp.StatusCode.ToString () == "OK") {
				label10.Text = "Online";
			} else {
				label10.Text = "Offline";
			}
		}
	}
}