using System;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EdgeSync.Common
{
	// Token: 0x02000031 RID: 49
	internal class XmlSerializationWriterStatus : XmlSerializationWriter
	{
		// Token: 0x0600011C RID: 284 RVA: 0x000063BE File Offset: 0x000045BE
		public void Write5_Status(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteNullTagLiteral("Status", "");
				return;
			}
			base.TopLevelElement();
			this.Write4_Status("Status", "", (Status)o, true, false);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000063F8 File Offset: 0x000045F8
		private void Write4_Status(string n, string ns, Status o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(Status)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("Status", "");
			}
			base.WriteElementString("Result", "", this.Write1_StatusResult(o.Result));
			base.WriteElementString("Type", "", this.Write2_SyncTreeType(o.Type));
			base.WriteElementString("Name", "", o.Name);
			base.WriteElementString("FailureDetails", "", o.FailureDetails);
			base.WriteElementStringRaw("StartUTC", "", XmlSerializationWriter.FromDateTime(o.StartUTC));
			base.WriteElementStringRaw("EndUTC", "", XmlSerializationWriter.FromDateTime(o.EndUTC));
			base.WriteElementStringRaw("Added", "", XmlConvert.ToString(o.Added));
			base.WriteElementStringRaw("Deleted", "", XmlConvert.ToString(o.Deleted));
			base.WriteElementStringRaw("Updated", "", XmlConvert.ToString(o.Updated));
			base.WriteElementStringRaw("Scanned", "", XmlConvert.ToString(o.Scanned));
			base.WriteElementStringRaw("TargetScanned", "", XmlConvert.ToString(o.TargetScanned));
			base.WriteEndElement(o);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000065A0 File Offset: 0x000047A0
		private string Write2_SyncTreeType(SyncTreeType v)
		{
			switch (v)
			{
			case SyncTreeType.Configuration:
				return "Configuration";
			case SyncTreeType.Recipients:
				return "Recipients";
			case SyncTreeType.General:
				return "General";
			}
			return XmlSerializationWriter.FromEnum((long)v, new string[]
			{
				"Configuration",
				"Recipients",
				"General"
			}, new long[]
			{
				1L,
				2L,
				4L
			}, "Microsoft.Exchange.EdgeSync.Common.SyncTreeType");
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00006624 File Offset: 0x00004824
		private string Write1_StatusResult(StatusResult v)
		{
			string result;
			switch (v)
			{
			case StatusResult.InProgress:
				result = "InProgress";
				break;
			case StatusResult.Success:
				result = "Success";
				break;
			case StatusResult.Aborted:
				result = "Aborted";
				break;
			case StatusResult.CouldNotConnect:
				result = "CouldNotConnect";
				break;
			case StatusResult.CouldNotLease:
				result = "CouldNotLease";
				break;
			case StatusResult.LostLease:
				result = "LostLease";
				break;
			case StatusResult.Incomplete:
				result = "Incomplete";
				break;
			case StatusResult.NoSubscriptions:
				result = "NoSubscriptions";
				break;
			case StatusResult.SyncDisabled:
				result = "SyncDisabled";
				break;
			default:
				throw base.CreateInvalidEnumValueException(((long)v).ToString(CultureInfo.InvariantCulture), "Microsoft.Exchange.EdgeSync.Common.StatusResult");
			}
			return result;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000066C5 File Offset: 0x000048C5
		protected override void InitCallbacks()
		{
		}
	}
}
