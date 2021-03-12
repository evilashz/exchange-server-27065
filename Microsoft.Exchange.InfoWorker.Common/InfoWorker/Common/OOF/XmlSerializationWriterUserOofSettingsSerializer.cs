using System;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.InfoWorker.Common.Availability;

namespace Microsoft.Exchange.InfoWorker.Common.OOF
{
	// Token: 0x02000024 RID: 36
	internal class XmlSerializationWriterUserOofSettingsSerializer : XmlSerializationWriter
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x0000456B File Offset: 0x0000276B
		public void Write7_UserOofSettings(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteNullTagLiteral("UserOofSettings", "");
				return;
			}
			base.TopLevelElement();
			this.Write6_UserOofSettingsSerializer("UserOofSettings", "", (UserOofSettingsSerializer)o, true, false);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000045A8 File Offset: 0x000027A8
		private void Write6_UserOofSettingsSerializer(string n, string ns, UserOofSettingsSerializer o, bool isNullable, bool needType)
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
				if (!(type == typeof(UserOofSettingsSerializer)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("UserOofSettingsSerializer", "");
			}
			this.Write2_Duration("Duration", "", o.Duration, false, false);
			base.WriteElementString("OofState", "", this.Write3_OofState(o.OofState));
			base.WriteElementString("ExternalAudience", "", this.Write4_ExternalAudience(o.ExternalAudience));
			this.Write5_ReplyBodySerializer("InternalReply", "", o.InternalReply, false, false);
			this.Write5_ReplyBodySerializer("ExternalReply", "", o.ExternalReply, false, false);
			base.WriteElementStringRaw("SetByLegacyClient", "", XmlConvert.ToString(o.SetByLegacyClient));
			if (o.UserChangeTime != null)
			{
				base.WriteNullableStringLiteralRaw("UserChangeTime", "", XmlSerializationWriter.FromDateTime(o.UserChangeTime.Value));
			}
			else
			{
				base.WriteNullTagLiteral("UserChangeTime", "");
			}
			base.WriteEndElement(o);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000046EC File Offset: 0x000028EC
		private void Write5_ReplyBodySerializer(string n, string ns, ReplyBodySerializer o, bool isNullable, bool needType)
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
				if (!(type == typeof(ReplyBodySerializer)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("ReplyBodySerializer", "");
			}
			base.WriteElementString("Message", "", o.Message);
			base.WriteElementString("LanguageTag", "", o.LanguageTag);
			base.WriteEndElement(o);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004780 File Offset: 0x00002980
		private string Write4_ExternalAudience(ExternalAudience v)
		{
			string result;
			switch (v)
			{
			case ExternalAudience.None:
				result = "None";
				break;
			case ExternalAudience.Known:
				result = "Known";
				break;
			case ExternalAudience.All:
				result = "All";
				break;
			default:
				throw base.CreateInvalidEnumValueException(((long)v).ToString(CultureInfo.InvariantCulture), "Microsoft.Exchange.InfoWorker.Common.OOF.ExternalAudience");
			}
			return result;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000047DC File Offset: 0x000029DC
		private string Write3_OofState(OofState v)
		{
			string result;
			switch (v)
			{
			case OofState.Disabled:
				result = "Disabled";
				break;
			case OofState.Enabled:
				result = "Enabled";
				break;
			case OofState.Scheduled:
				result = "Scheduled";
				break;
			default:
				throw base.CreateInvalidEnumValueException(((long)v).ToString(CultureInfo.InvariantCulture), "Microsoft.Exchange.InfoWorker.Common.OOF.OofState");
			}
			return result;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004838 File Offset: 0x00002A38
		private void Write2_Duration(string n, string ns, Duration o, bool isNullable, bool needType)
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
				if (!(type == typeof(Duration)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("Duration", "http://schemas.microsoft.com/exchange/services/2006/types");
			}
			base.WriteElementStringRaw("StartTime", "http://schemas.microsoft.com/exchange/services/2006/types", XmlSerializationWriter.FromDateTime(o.StartTime));
			base.WriteElementStringRaw("EndTime", "http://schemas.microsoft.com/exchange/services/2006/types", XmlSerializationWriter.FromDateTime(o.EndTime));
			base.WriteEndElement(o);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000048D6 File Offset: 0x00002AD6
		protected override void InitCallbacks()
		{
		}
	}
}
