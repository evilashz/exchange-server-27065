using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004CD RID: 1229
	[OwaEventNamespace("ReadADOrgPerson")]
	internal sealed class ReadADOrgPersonEventHandler : OwaEventHandlerBase
	{
		// Token: 0x06002EEA RID: 12010 RVA: 0x0010DE18 File Offset: 0x0010C018
		public static void Register()
		{
			OwaEventRegistry.RegisterHandler(typeof(ReadADOrgPersonEventHandler));
		}

		// Token: 0x06002EEB RID: 12011 RVA: 0x0010DE2C File Offset: 0x0010C02C
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEventParameter("id", typeof(ADObjectId))]
		[OwaEventParameter("SD", typeof(ExDateTime), false, true)]
		[OwaEventParameter("ED", typeof(ExDateTime), false, true)]
		[OwaEventParameter("EA", typeof(string), false, true)]
		[OwaEvent("LID")]
		public void LoadInitialData()
		{
			ExTraceGlobals.OehCallTracer.TraceDebug((long)this.GetHashCode(), "ReadADOrgPersonEventHandler.LoadInitialData");
			IRecipientSession recipientSession = Utilities.CreateADRecipientSession(Culture.GetUserCulture().LCID, true, ConsistencyMode.IgnoreInvalid, true, base.UserContext, false);
			IADOrgPerson iadorgPerson = recipientSession.Read((ADObjectId)base.GetParameter("id")) as IADOrgPerson;
			if (iadorgPerson == null)
			{
				throw new OwaInvalidRequestException("couldn't find person");
			}
			StringBuilder stringBuilder = new StringBuilder();
			StringWriter stringWriter = new StringWriter(stringBuilder);
			ReadADOrgPerson.RenderOrganizationContents(stringWriter, base.UserContext, iadorgPerson, recipientSession);
			stringWriter.Close();
			this.Writer.Write("sOrg = '");
			Utilities.JavascriptEncode(stringBuilder.ToString(), this.Writer);
			this.Writer.Write("';");
			if (base.IsParameterSet("EA") && base.UserContext.IsFeatureEnabled(Feature.Calendar))
			{
				string text = this.RenderFreeBusyData((string)base.GetParameter("EA"), (ExDateTime)base.GetParameter("SD"), (ExDateTime)base.GetParameter("ED"), true);
				if (text != null)
				{
					this.Writer.Write("sFBErr = \"");
					Utilities.JavascriptEncode(text, this.Writer);
					this.Writer.Write("\";");
				}
			}
		}

		// Token: 0x06002EEC RID: 12012 RVA: 0x0010DF6C File Offset: 0x0010C16C
		[OwaEventParameter("EA", typeof(string))]
		[OwaEventParameter("SD", typeof(ExDateTime))]
		[OwaEventParameter("ED", typeof(ExDateTime))]
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEvent("GetFreeBusy")]
		[OwaEventSegmentation(Feature.Calendar)]
		public void GetFreeBusyData()
		{
			ExTraceGlobals.OehCallTracer.TraceDebug((long)this.GetHashCode(), "ReadADOrgPersonEventHandler.GetFreeBusy");
			string text = this.RenderFreeBusyData((string)base.GetParameter("EA"), (ExDateTime)base.GetParameter("SD"), (ExDateTime)base.GetParameter("ED"), false);
			if (text != null)
			{
				throw new OwaEventHandlerException("Unable to get free busy data", text);
			}
		}

		// Token: 0x06002EED RID: 12013 RVA: 0x0010DFD8 File Offset: 0x0010C1D8
		private string RenderFreeBusyData(string smtpAddress, ExDateTime startDate, ExDateTime endDate, bool renderOof)
		{
			string value;
			string value2;
			string text;
			string freeBusy = ReadADOrgPerson.GetFreeBusy(base.OwaContext, smtpAddress, startDate, endDate, this.HttpContext, out value, out value2, out text);
			if (freeBusy != null)
			{
				return freeBusy;
			}
			this.Writer.Write("rgFbd = new Array('");
			this.Writer.Write(value);
			this.Writer.Write("','");
			this.Writer.Write(value2);
			this.Writer.Write("');");
			if (renderOof && text != null)
			{
				this.Writer.Write("sOof = \"");
				Utilities.JavascriptEncode(text, this.Writer);
				this.Writer.Write("\";");
			}
			return null;
		}

		// Token: 0x040020CD RID: 8397
		public const string EventNamespace = "ReadADOrgPerson";

		// Token: 0x040020CE RID: 8398
		public const string MethodGetFreeBusyData = "GetFreeBusy";

		// Token: 0x040020CF RID: 8399
		public const string MethodLoadInitalData = "LID";

		// Token: 0x040020D0 RID: 8400
		public const string Id = "id";

		// Token: 0x040020D1 RID: 8401
		public const string StartDate = "SD";

		// Token: 0x040020D2 RID: 8402
		public const string EndDate = "ED";

		// Token: 0x040020D3 RID: 8403
		public const string EmailAddress = "EA";
	}
}
