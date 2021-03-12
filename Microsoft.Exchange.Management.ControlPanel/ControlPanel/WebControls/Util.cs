using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000678 RID: 1656
	public static class Util
	{
		// Token: 0x0600479D RID: 18333 RVA: 0x000D9F0C File Offset: 0x000D810C
		static Util()
		{
			try
			{
				Util.isMicrosoftHostedOnly = Datacenter.IsMicrosoftHostedOnly(true);
			}
			catch (CannotDetermineExchangeModeException)
			{
				Util.isMicrosoftHostedOnly = false;
			}
			try
			{
				Util.isPartnerHostedOnly = Datacenter.IsPartnerHostedOnly(true);
			}
			catch (CannotDetermineExchangeModeException)
			{
				Util.isPartnerHostedOnly = false;
			}
			Util.isDataCenter = Datacenter.IsMultiTenancyEnabled();
		}

		// Token: 0x1700277D RID: 10109
		// (get) Token: 0x0600479E RID: 18334 RVA: 0x000D9F6C File Offset: 0x000D816C
		internal static string OwaApplicationVersion
		{
			get
			{
				return AssemblyUtil.OwaAppVersion;
			}
		}

		// Token: 0x1700277E RID: 10110
		// (get) Token: 0x0600479F RID: 18335 RVA: 0x000D9F73 File Offset: 0x000D8173
		internal static string ApplicationVersion
		{
			get
			{
				if (Util.applicationVersion == null)
				{
					Util.applicationVersion = typeof(Util).GetApplicationVersion();
				}
				return Util.applicationVersion;
			}
		}

		// Token: 0x1700277F RID: 10111
		// (get) Token: 0x060047A0 RID: 18336 RVA: 0x000D9F95 File Offset: 0x000D8195
		internal static bool IsDataCenter
		{
			get
			{
				return Util.isDataCenter;
			}
		}

		// Token: 0x17002780 RID: 10112
		// (get) Token: 0x060047A1 RID: 18337 RVA: 0x000D9F9C File Offset: 0x000D819C
		internal static bool IsMicrosoftHostedOnly
		{
			get
			{
				return Util.isMicrosoftHostedOnly;
			}
		}

		// Token: 0x17002781 RID: 10113
		// (get) Token: 0x060047A2 RID: 18338 RVA: 0x000D9FA3 File Offset: 0x000D81A3
		internal static bool IsPartnerHostedOnly
		{
			get
			{
				return Util.isPartnerHostedOnly;
			}
		}

		// Token: 0x17002782 RID: 10114
		// (get) Token: 0x060047A3 RID: 18339 RVA: 0x000D9FAA File Offset: 0x000D81AA
		public static string CultureName
		{
			get
			{
				return Thread.CurrentThread.CurrentUICulture.Name;
			}
		}

		// Token: 0x060047A4 RID: 18340 RVA: 0x000D9FBB File Offset: 0x000D81BB
		internal static string GetPageTitleFormat(SiteMapNode node)
		{
			if (!Util.isMicrosoftHostedOnly)
			{
				return node["entFormat"];
			}
			return node["liveFormat"];
		}

		// Token: 0x060047A5 RID: 18341 RVA: 0x000D9FDB File Offset: 0x000D81DB
		internal static string ConvertBoolToYesNo(bool value)
		{
			if (!value)
			{
				return "no";
			}
			return "yes";
		}

		// Token: 0x060047A6 RID: 18342 RVA: 0x000D9FEC File Offset: 0x000D81EC
		[Conditional("DEBUG")]
		internal static void EnsureScriptManager(Control c)
		{
			Page page = (c as Page) ?? c.Page;
			if (ScriptManager.GetCurrent(page) == null)
			{
				throw new InvalidOperationException(string.Format("The control with ID '{0}' requires a ScriptManager on the page. The ScriptManager must appear before any controls that need it.", c.ID));
			}
		}

		// Token: 0x060047A7 RID: 18343 RVA: 0x000DA028 File Offset: 0x000D8228
		internal static void EndResponse(HttpResponse response, HttpStatusCode statusCode)
		{
			response.Clear();
			response.StatusCode = (int)statusCode;
			response.End();
		}

		// Token: 0x060047A8 RID: 18344 RVA: 0x000DA040 File Offset: 0x000D8240
		internal static EncodingLabel CreateHiddenForSRLabel(string text, string forCtrlId)
		{
			return new EncodingLabel
			{
				ID = forCtrlId + "_label",
				Text = text,
				CssClass = "HiddenForScreenReader",
				AssociatedControlID = forCtrlId
			};
		}

		// Token: 0x060047A9 RID: 18345 RVA: 0x000DA080 File Offset: 0x000D8280
		internal static void MakeControlRbacDisabled(Control c)
		{
			if (c is EcpCollectionEditor)
			{
				((EcpCollectionEditor)c).ReadOnly = true;
			}
			else if (c is InlineEditor)
			{
				((InlineEditor)c).ReadOnly = true;
			}
			else if (c is TextBox)
			{
				((TextBox)c).ReadOnly = true;
				TextBox textBox = (TextBox)c;
				textBox.CssClass += " ReadOnly";
			}
			else if (c is WebControl)
			{
				((WebControl)c).Enabled = false;
			}
			else
			{
				if (!(c is HtmlControl))
				{
					throw new ArgumentException("Cannot make the control readonly");
				}
				((HtmlControl)c).Disabled = true;
			}
			IAttributeAccessor attributeAccessor = c as IAttributeAccessor;
			if (attributeAccessor != null)
			{
				Util.MarkRBACDisabled(attributeAccessor);
			}
			if (c is RadioButton)
			{
				string groupName = ((RadioButton)c).GroupName;
				foreach (RadioButton radioButton in Util.FindControls<RadioButton>(c.NamingContainer))
				{
					if (string.Equals(radioButton.GroupName, groupName, StringComparison.OrdinalIgnoreCase) && radioButton != c)
					{
						radioButton.Enabled = false;
						Util.MarkRBACDisabled(radioButton);
					}
				}
			}
		}

		// Token: 0x060047AA RID: 18346 RVA: 0x000DA1AF File Offset: 0x000D83AF
		internal static IEnumerable<T> FindControls<T>(Control parent) where T : Control
		{
			return Util.FindControls(parent, (Control x) => x is T).Cast<T>();
		}

		// Token: 0x060047AB RID: 18347 RVA: 0x000DA368 File Offset: 0x000D8568
		internal static IEnumerable<Control> FindControls(Control parent, Predicate<Control> predicate)
		{
			Stack<Control> stack = new Stack<Control>();
			stack.Push(parent);
			while (stack.Count > 0)
			{
				Control c = stack.Pop();
				if (c.HasControls())
				{
					foreach (object obj in c.Controls)
					{
						Control item = (Control)obj;
						stack.Push(item);
					}
				}
				if (predicate(c))
				{
					yield return c;
				}
			}
			yield break;
		}

		// Token: 0x060047AC RID: 18348 RVA: 0x000DA38C File Offset: 0x000D858C
		internal static void MarkRBACDisabled(IAttributeAccessor attributeAccessor)
		{
			attributeAccessor.SetAttribute("rbacDisabled", "true");
		}

		// Token: 0x060047AD RID: 18349 RVA: 0x000DA39E File Offset: 0x000D859E
		internal static bool IsChrome()
		{
			return HttpContext.Current.Request.Browser.IsBrowser("Chrome");
		}

		// Token: 0x060047AE RID: 18350 RVA: 0x000DA3B9 File Offset: 0x000D85B9
		internal static bool IsSafari()
		{
			return HttpContext.Current.Request.Browser.IsBrowser("Safari");
		}

		// Token: 0x060047AF RID: 18351 RVA: 0x000DA3D4 File Offset: 0x000D85D4
		internal static bool IsIE()
		{
			return (HttpContext.Current.Request.UserAgent != null && HttpContext.Current.Request.UserAgent.IndexOf("Trident", StringComparison.OrdinalIgnoreCase) > -1) || HttpContext.Current.Request.Browser.IsBrowser("IE");
		}

		// Token: 0x060047B0 RID: 18352 RVA: 0x000DA42A File Offset: 0x000D862A
		internal static bool IsFirefox()
		{
			return HttpContext.Current.Request.Browser.IsBrowser("Firefox");
		}

		// Token: 0x060047B1 RID: 18353 RVA: 0x000DA445 File Offset: 0x000D8645
		public static void RenderCultureName(Control ctrl)
		{
			ctrl.Page.Response.Output.Write(Util.CultureName);
		}

		// Token: 0x060047B2 RID: 18354 RVA: 0x000DA464 File Offset: 0x000D8664
		internal static string GetLCID()
		{
			return Thread.CurrentThread.CurrentUICulture.LCID.ToString("X");
		}

		// Token: 0x060047B3 RID: 18355 RVA: 0x000DA490 File Offset: 0x000D8690
		internal static string GetLCIDInDecimal()
		{
			return Thread.CurrentThread.CurrentUICulture.LCID.ToString("d");
		}

		// Token: 0x060047B4 RID: 18356 RVA: 0x000DA4B9 File Offset: 0x000D86B9
		public static void RenderLCIDInDecimal(Control ctrl)
		{
			ctrl.Page.Response.Output.Write(Util.GetLCIDInDecimal());
		}

		// Token: 0x060047B5 RID: 18357 RVA: 0x000DA4D5 File Offset: 0x000D86D5
		public static void RenderLCID(Control ctrl)
		{
			ctrl.Page.Response.Output.Write(Util.GetLCID());
		}

		// Token: 0x060047B6 RID: 18358 RVA: 0x000DA4F4 File Offset: 0x000D86F4
		public static void RenderLocStringAndLCID(Control ctrl, string stringId)
		{
			string @string = EcpGlobalResourceProvider.ResourceManager.GetString(stringId, Thread.CurrentThread.CurrentUICulture);
			ctrl.Page.Response.Output.Write(@string, Util.GetLCID());
		}

		// Token: 0x060047B7 RID: 18359 RVA: 0x000DA534 File Offset: 0x000D8734
		public static void RenderLocStringAndLCIDForOwaOption(Control ctrl, string stringId)
		{
			string @string = EcpGlobalResourceProvider.OwaOptionResourceManager.GetString(stringId, Thread.CurrentThread.CurrentUICulture);
			ctrl.Page.Response.Output.Write(@string, Util.GetLCID());
		}

		// Token: 0x060047B8 RID: 18360 RVA: 0x000DA574 File Offset: 0x000D8774
		internal static void NotifyOWAUserSettingsChanged(UserSettings userSettings)
		{
			HttpCookieCollection cookies = HttpContext.Current.Response.Cookies;
			string name = "EcpUpdatedUserSettings";
			int num = (int)userSettings;
			cookies.Add(new HttpCookie(name, num.ToString())
			{
				HttpOnly = false
			});
		}

		// Token: 0x060047B9 RID: 18361 RVA: 0x000DA5B4 File Offset: 0x000D87B4
		internal static int GetMaxLengthFromDefinition(ProviderPropertyDefinition propDefinition)
		{
			int num = int.MaxValue;
			foreach (PropertyDefinitionConstraint propertyDefinitionConstraint in propDefinition.AllConstraints)
			{
				StringLengthConstraint stringLengthConstraint = propertyDefinitionConstraint as StringLengthConstraint;
				if (stringLengthConstraint != null && stringLengthConstraint.MaxLength < num)
				{
					num = stringLengthConstraint.MaxLength;
				}
			}
			return num;
		}

		// Token: 0x060047BA RID: 18362 RVA: 0x000DA624 File Offset: 0x000D8824
		public static string GetSpriteImageSrc(Control c)
		{
			return ThemeResource.Private_GetThemeResource(c, "clear1x1.gif");
		}

		// Token: 0x060047BB RID: 18363 RVA: 0x000DA66C File Offset: 0x000D886C
		public static void RequireUpdateProgressPopUp(Control control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			control.Init += delegate(object param0, EventArgs param1)
			{
				control.Page.InitComplete += delegate(object param0, EventArgs param1)
				{
					UpdateProgressPopUp.GetCurrent(control.Page);
				};
			};
		}

		// Token: 0x060047BC RID: 18364 RVA: 0x000DA6B0 File Offset: 0x000D88B0
		public static void RenderXDomainChecker(HttpResponse response)
		{
			if (!Util.IsDataCenter && response.Headers["X-Frame-Options"] == "SameOrigin")
			{
				response.Headers.Remove("X-Frame-Options");
				response.Write("<script type=\"text/javascript\"><!-- if (window.self != window.top) { try { var parentLocation = window.parent.location.toString(); } catch(e) { window.location = \"/ecp/error.aspx?cause=DenyCrossDomainHost\"; } } --></script>");
			}
		}

		// Token: 0x04003029 RID: 12329
		internal const string EcpUpdatedUserSettingsKey = "EcpUpdatedUserSettings";

		// Token: 0x0400302A RID: 12330
		internal const string DoNothingScript = "javascript:return false;";

		// Token: 0x0400302B RID: 12331
		private const string RbacDisabled = "rbacDisabled";

		// Token: 0x0400302C RID: 12332
		private static readonly bool isDataCenter;

		// Token: 0x0400302D RID: 12333
		private static readonly bool isMicrosoftHostedOnly;

		// Token: 0x0400302E RID: 12334
		private static readonly bool isPartnerHostedOnly;

		// Token: 0x0400302F RID: 12335
		private static string applicationVersion;
	}
}
