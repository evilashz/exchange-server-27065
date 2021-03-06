using System;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Protocols.Smtp;
using Microsoft.Exchange.Transport.Categorizer;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000448 RID: 1096
	internal class InboundExch50 : Exch50
	{
		// Token: 0x06003287 RID: 12935 RVA: 0x000C5CD6 File Offset: 0x000C3ED6
		public InboundExch50(TransportMailItem mailItem, ISmtpInServer server, IMailRouter mailRouter, ITransportAppConfig transportAppConfig)
		{
			this.mailItem = mailItem;
			this.suppress = null;
			this.server = server;
			this.mailRouter = mailRouter;
			this.transportAppConfig = transportAppConfig;
		}

		// Token: 0x06003288 RID: 12936 RVA: 0x000C5D08 File Offset: 0x000C3F08
		public bool ProcessExch50(byte[] data, RecipientCorrelator recipientCorrelator)
		{
			bool flag = true;
			Exch50Reader exch50Reader = new Exch50Reader(data, 0, data.Length);
			if (!exch50Reader.ReadNextBlob())
			{
				return false;
			}
			try
			{
				MdbefPropertyCollection mdbefProperties = exch50Reader.GetMdbefProperties();
				if (mdbefProperties != null && !this.ProcessMessageMdbef(mdbefProperties))
				{
					flag = false;
				}
			}
			catch (MdbefException)
			{
				flag = false;
			}
			foreach (MailRecipient mailRecipient in recipientCorrelator)
			{
				if (!exch50Reader.ReadNextBlob())
				{
					return false;
				}
				if (mailRecipient != null)
				{
					try
					{
						MdbefPropertyCollection mdbefProperties2 = exch50Reader.GetMdbefProperties();
						if (mdbefProperties2 != null && !this.ProcessRecipientMdbef(mailRecipient, mdbefProperties2))
						{
							flag = false;
						}
					}
					catch (MdbefException)
					{
						flag = false;
					}
				}
			}
			if (flag && exch50Reader.ReadNextBlob())
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06003289 RID: 12937 RVA: 0x000C5DDC File Offset: 0x000C3FDC
		public void PatchHeaders(HeaderList headers, bool shouldPatchArrivalTime)
		{
			if (this.scl != null)
			{
				InboundExch50.ApplyScl(this.scl.Value, headers);
			}
			if (this.senderIdStatus != null)
			{
				InboundExch50.ApplySenderIdStatus(this.senderIdStatus.Value, headers);
			}
			if (this.suppress != null)
			{
				InboundExch50.ApplyAutoResponseSuppress(this.suppress.Value, headers);
			}
			if (shouldPatchArrivalTime && this.arrivalTime != null)
			{
				InboundExch50.ApplyOrganizationalMessageArrivalTime(this.arrivalTime.Value, headers);
			}
			if (this.suppressRecallReport)
			{
				InboundExch50.ApplySuppressRecallReport(headers);
			}
			if (this.bifInfo != null)
			{
				InboundExch50.ApplyContentConversionOptions(this.bifInfo, headers);
			}
			InboundExch50.ApplyProhibitExpansion(this.dlExpansionProhibited, this.altRecipientProhibited, headers);
		}

		// Token: 0x0600328A RID: 12938 RVA: 0x000C5E98 File Offset: 0x000C4098
		private static void ApplyContentConversionOptions(BifInfo bifInfo, HeaderList headers)
		{
			Header header = headers.FindFirst("X-MS-Exchange-Organization-ContentConversionOptions");
			if (header != null)
			{
				return;
			}
			string contentConversionOptionsString = bifInfo.GetContentConversionOptionsString();
			if (!string.IsNullOrEmpty(contentConversionOptionsString))
			{
				Header newChild = new AsciiTextHeader("X-MS-Exchange-Organization-ContentConversionOptions", contentConversionOptionsString);
				headers.AppendChild(newChild);
			}
		}

		// Token: 0x0600328B RID: 12939 RVA: 0x000C5ED8 File Offset: 0x000C40D8
		private static void ApplyScl(int scl, HeaderList headers)
		{
			headers.RemoveAll("X-MS-Exchange-Organization-SCL");
			Header newChild = new AsciiTextHeader("X-MS-Exchange-Organization-SCL", scl.ToString(NumberFormatInfo.InvariantInfo));
			headers.AppendChild(newChild);
		}

		// Token: 0x0600328C RID: 12940 RVA: 0x000C5F10 File Offset: 0x000C4110
		private static void ApplySenderIdStatus(SenderIdStatus status, HeaderList headers)
		{
			headers.RemoveAll("X-MS-Exchange-Organization-SenderIdResult");
			Header newChild = new AsciiTextHeader("X-MS-Exchange-Organization-SenderIdResult", status.ToString());
			headers.AppendChild(newChild);
		}

		// Token: 0x0600328D RID: 12941 RVA: 0x000C5F48 File Offset: 0x000C4148
		private static void ApplyProhibitExpansion(bool dlExpansionProhibited, bool altRecipientProhibited, HeaderList headers)
		{
			if (dlExpansionProhibited && headers.FindFirst("X-MS-Exchange-Organization-DL-Expansion-Prohibited") == null)
			{
				headers.AppendChild(new AsciiTextHeader("X-MS-Exchange-Organization-DL-Expansion-Prohibited", string.Empty));
			}
			if (altRecipientProhibited && headers.FindFirst("X-MS-Exchange-Organization-Alt-Recipient-Prohibited") == null)
			{
				headers.AppendChild(new AsciiTextHeader("X-MS-Exchange-Organization-Alt-Recipient-Prohibited", string.Empty));
			}
		}

		// Token: 0x0600328E RID: 12942 RVA: 0x000C5FA4 File Offset: 0x000C41A4
		private static void ApplyAutoResponseSuppress(AutoResponseSuppress suppress, HeaderList headers)
		{
			if (suppress == (AutoResponseSuppress)0)
			{
				return;
			}
			AutoResponseSuppress autoResponseSuppress = (AutoResponseSuppress)0;
			AutoResponseSuppress autoResponseSuppress2 = (AutoResponseSuppress)0;
			Header[] array = headers.FindAll("X-Auto-Response-Suppress");
			foreach (Header header in array)
			{
				try
				{
					if (EnumValidator<AutoResponseSuppress>.TryParse(header.Value, EnumParseOptions.IgnoreCase, out autoResponseSuppress2))
					{
						autoResponseSuppress |= autoResponseSuppress2;
					}
				}
				catch (ExchangeDataException)
				{
				}
			}
			if ((suppress & ~(autoResponseSuppress != (AutoResponseSuppress)0)) == (AutoResponseSuppress)0)
			{
				return;
			}
			AutoResponseSuppress autoResponseSuppress3 = suppress | autoResponseSuppress;
			if (array.Length > 0)
			{
				for (int j = 1; j < array.Length; j++)
				{
					headers.RemoveChild(array[j]);
				}
				array[0].Value = autoResponseSuppress3.ToString();
			}
			else
			{
				Header newChild = new AsciiTextHeader("X-Auto-Response-Suppress", autoResponseSuppress3.ToString());
				headers.AppendChild(newChild);
			}
			if ((autoResponseSuppress3 & AutoResponseSuppress.RN) != (AutoResponseSuppress)0)
			{
				headers.RemoveAll(HeaderId.DispositionNotificationTo);
				headers.RemoveAll(HeaderId.ReturnReceiptTo);
			}
		}

		// Token: 0x0600328F RID: 12943 RVA: 0x000C6088 File Offset: 0x000C4288
		private static void ApplyOrganizationalMessageArrivalTime(DateTime time, HeaderList headers)
		{
			string value = Util.FormatOrganizationalMessageArrivalTime(time);
			Header header = headers.FindFirst("X-MS-Exchange-Organization-OriginalArrivalTime");
			if (header != null)
			{
				header.Value = value;
				return;
			}
			headers.AppendChild(new AsciiTextHeader("X-MS-Exchange-Organization-OriginalArrivalTime", value));
		}

		// Token: 0x06003290 RID: 12944 RVA: 0x000C60C5 File Offset: 0x000C42C5
		private static void ApplySuppressRecallReport(HeaderList headers)
		{
			ResolverMessage.SetSuppressRecallReport(headers, true);
		}

		// Token: 0x06003291 RID: 12945 RVA: 0x000C60D0 File Offset: 0x000C42D0
		private static byte[] FilterMessageInternalTranProps(ExchangeMailMsgProperty id, byte[] value)
		{
			switch (id)
			{
			case ExchangeMailMsgProperty.IMMPID_EMP_INTERNET_ADDRESS_CONVERSION:
			case ExchangeMailMsgProperty.IMMPID_EMP_SYSTEM_MESSAGE_CLASS:
			case ExchangeMailMsgProperty.IMMPID_EMP_SENDER_RECIPIENT_LIMIT:
			case ExchangeMailMsgProperty.IMMPID_EMP_DL_OWNER_DN:
			case ExchangeMailMsgProperty.IMMPID_EMP_P1_SENDER_DN:
				return null;
			}
			return value;
		}

		// Token: 0x06003292 RID: 12946 RVA: 0x000C610C File Offset: 0x000C430C
		private static byte[] FilterMessageExternalTranProps(ExchangeMailMsgProperty id, byte[] value)
		{
			if (id == ExchangeMailMsgProperty.IMMPID_EMP_SENDER_RECIPIENT_LIMIT)
			{
				return null;
			}
			return value;
		}

		// Token: 0x06003293 RID: 12947 RVA: 0x000C6124 File Offset: 0x000C4324
		private static bool TryDecodeMessageTrackingOrgGuid(byte[] data, out Guid orgGuid, out DateTime time)
		{
			orgGuid = Guid.Empty;
			time = DateTime.MinValue;
			if (data.Length != 32)
			{
				return false;
			}
			orgGuid = ExBitConverter.ReadGuid(data, 0);
			int year = (int)BitConverter.ToInt16(data, 16);
			int month = (int)BitConverter.ToInt16(data, 18);
			int day = (int)BitConverter.ToInt16(data, 22);
			int hour = (int)BitConverter.ToInt16(data, 24);
			int minute = (int)BitConverter.ToInt16(data, 26);
			int second = (int)BitConverter.ToInt16(data, 28);
			int millisecond = (int)BitConverter.ToInt16(data, 30);
			try
			{
				time = new DateTime(year, month, day, hour, minute, second, millisecond, DateTimeKind.Utc);
			}
			catch (ArgumentException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06003294 RID: 12948 RVA: 0x000C61D4 File Offset: 0x000C43D4
		private static bool TryDecodeLongAddress(string value, RoutingAddress p1Address, out RoutingAddress address)
		{
			address = RoutingAddress.Empty;
			string domainPart = p1Address.DomainPart;
			if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(domainPart))
			{
				return false;
			}
			ProxyAddress proxyAddress = ProxyAddress.Parse(value);
			SmtpProxyAddress smtpProxyAddress;
			if (!(proxyAddress is InvalidProxyAddress) && proxyAddress.Prefix == ProxyAddressPrefix.X400 && SmtpProxyAddress.TryEncapsulate(proxyAddress, domainPart, out smtpProxyAddress))
			{
				address = (RoutingAddress)smtpProxyAddress.SmtpAddress;
				return true;
			}
			ExTraceGlobals.SmtpReceiveTracer.TraceError<string, string>(0L, "Failed to decode EXCH50 long address '{0}' corresponding to P1 address '{1}'", value, p1Address.ToString());
			return false;
		}

		// Token: 0x06003295 RID: 12949 RVA: 0x000C6268 File Offset: 0x000C4468
		private bool ProcessMessageMdbef(MdbefPropertyCollection mdbef)
		{
			bool result = true;
			bool flag = false;
			object obj;
			if (mdbef.TryGetValue(1081475075U, out obj))
			{
				int num = (int)obj;
				this.scl = new int?((num < -1) ? -1 : ((num > 9) ? 9 : num));
				mdbef.Remove(1081475075U);
			}
			if (mdbef.TryGetValue(1081671683U, out obj))
			{
				this.senderIdStatus = new SenderIdStatus?((SenderIdStatus)((int)obj));
				mdbef.Remove(1081671683U);
			}
			if (mdbef.TryGetValue(1071579139U, out obj))
			{
				this.suppress = new AutoResponseSuppress?((AutoResponseSuppress)((int)obj));
				mdbef.Remove(1071579139U);
			}
			if (mdbef.TryGetValue(1703966U, out obj))
			{
				this.messageClass = (string)obj;
				mdbef.Remove(1703966U);
			}
			if (mdbef.TryGetValue(524318U, out obj))
			{
				this.mailItem.ExtendedProperties.SetValue<string>("Microsoft.Exchange.ContentIdentifier", (string)obj);
				mdbef.Remove(524318U);
				if (string.Compare((string)obj, "EXJournalData", StringComparison.OrdinalIgnoreCase) == 0)
				{
					flag = true;
				}
			}
			if (mdbef.TryGetValue(1080819970U, out obj))
			{
				byte[] array = (byte[])obj;
				int num2 = array.Length / 16;
				Guid[] array2 = new Guid[num2];
				for (int i = 0; i < num2; i++)
				{
					array2[i] = ExBitConverter.ReadGuid(array, 16 * i);
				}
				this.mailItem.ExtendedProperties.SetValue<Guid[]>("Microsoft.Exchange.JournalRecipientList", array2);
				mdbef.Remove(1080819970U);
			}
			if (mdbef.TryGetValue(1310731U, out obj))
			{
				this.dlExpansionProhibited = (bool)obj;
			}
			if (mdbef.TryGetValue(2818059U, out obj))
			{
				this.altRecipientProhibited = (this.transportAppConfig.Resolver.EnableForwardingProhibitedFeature && (bool)obj);
			}
			RoutingAddress routingAddress;
			if (mdbef.TryGetValue(1081540638U, out obj) && InboundExch50.TryDecodeLongAddress((string)obj, this.mailItem.From, out routingAddress))
			{
				if (!Util.IsLongAddress(this.mailItem.From))
				{
					this.mailItem.From = routingAddress;
				}
				else if (this.mailItem.From != routingAddress)
				{
					ExTraceGlobals.SmtpReceiveTracer.TraceError<string, string>(0L, "Ignoring EXCH50 long sender address '{0}' giving precedence to MAIL FROM address '{1}'", routingAddress.ToString(), this.mailItem.From.ToString());
				}
				mdbef.Remove(1081540638U);
			}
			if (this.server.IsBridgehead)
			{
				if (mdbef.TryGetValue(1080754434U, out obj))
				{
					Guid a;
					DateTime value;
					bool flag2 = InboundExch50.TryDecodeMessageTrackingOrgGuid((byte[])obj, out a, out value);
					if (flag2 && a == this.server.Configuration.TransportSettings.OrganizationGuid)
					{
						this.arrivalTime = new DateTime?(value);
					}
				}
				if (mdbef.TryGetValue(1080951042U, out obj))
				{
					byte[] data = (byte[])obj;
					if (flag)
					{
						Exch50.CopyJournalProperties(data, this.mailItem);
					}
					try
					{
						mdbef[1080951042U] = Exch50.FilterTransportProperties(data, new Exch50.TransportPropertyFilter(InboundExch50.FilterMessageInternalTranProps));
					}
					catch (TransportPropertyException)
					{
						mdbef.Remove(1080951042U);
						result = false;
					}
				}
				if (mdbef.TryGetValue(1081606402U, out obj))
				{
					byte[] data2 = (byte[])obj;
					try
					{
						mdbef[1081606402U] = Exch50.FilterTransportProperties(data2, new Exch50.TransportPropertyFilter(InboundExch50.FilterMessageExternalTranProps));
					}
					catch (TransportPropertyException)
					{
						mdbef.Remove(1081606402U);
						result = false;
					}
				}
				if (mdbef.TryGetValue(1080688898U, out obj))
				{
					this.bifInfo = BifInfo.FromByteArray((byte[])obj);
					if (!string.IsNullOrEmpty(this.messageClass) && this.messageClass.Equals("IPM.Outlook.Recall", StringComparison.OrdinalIgnoreCase) && this.bifInfo.SenderType != null && this.bifInfo.SenderType.Value != BifSenderType.Originator)
					{
						this.suppressRecallReport = true;
					}
				}
			}
			if (mdbef.Count > 0)
			{
				this.mailItem.LegacyXexch50Blob = mdbef.GetBytes();
			}
			return result;
		}

		// Token: 0x06003296 RID: 12950 RVA: 0x000C6694 File Offset: 0x000C4894
		private bool ProcessRecipientMdbef(MailRecipient recipient, MdbefPropertyCollection mdbef)
		{
			bool result = true;
			object obj;
			RoutingAddress routingAddress;
			if (mdbef.TryGetValue(1476722718U, out obj) && InboundExch50.TryDecodeLongAddress((string)obj, recipient.Email, out routingAddress))
			{
				if (!Util.IsLongAddress(recipient.Email))
				{
					recipient.Email = routingAddress;
				}
				else if (recipient.Email != routingAddress)
				{
					ExTraceGlobals.SmtpReceiveTracer.TraceError<string, string>(0L, "Ignoring EXCH50 long recipient address '{0}' giving precedence to RCPT TO address '{1}'", routingAddress.ToString(), recipient.Email.ToString());
				}
				mdbef.Remove(1476722718U);
			}
			if (this.server.IsBridgehead)
			{
				if (mdbef.TryGetValue(1080885506U, out obj))
				{
					byte[] rawReplicaDn = (byte[])obj;
					string text;
					if (this.TryDecodePFReplica(rawReplicaDn, out text))
					{
						string text2;
						if (!recipient.ExtendedProperties.TryGetValue<string>("Microsoft.Exchange.Transport.RoutingOverride", out text2))
						{
							recipient.ExtendedProperties.SetValue<string>("Microsoft.Exchange.Transport.RoutingOverride", text);
						}
						else if (!text.Equals(text2, StringComparison.OrdinalIgnoreCase))
						{
							ExTraceGlobals.SmtpReceiveTracer.TraceError<string, string>(0L, "Ignoring EXCH50 PF Routing override '{0}' giving precedence to RCPT TO XRDST={1}", text, text2);
						}
					}
					mdbef.Remove(1080885506U);
				}
				if (mdbef.TryGetValue(1080951042U, out obj))
				{
					byte[] data = (byte[])obj;
					try
					{
						InboundExch50.RecipientInternalTranPropFilter @object = new InboundExch50.RecipientInternalTranPropFilter(recipient);
						mdbef[1080951042U] = Exch50.FilterTransportProperties(data, new Exch50.TransportPropertyFilter(@object.Filter));
					}
					catch (TransportPropertyException)
					{
						mdbef.Remove(1080951042U);
						result = false;
					}
				}
			}
			if (mdbef.TryGetValue(201916674U, out obj))
			{
				RoutingAddress routingAddress2;
				if (!OrarGenerator.TryGetOrarAddress(recipient, out routingAddress2))
				{
					OrarGenerator.SetOrarBlob(recipient, (byte[])obj);
				}
				else
				{
					ExTraceGlobals.SmtpReceiveTracer.TraceError<string, string>(0L, "Ignoring EXCH50 ORAR for recipient '{0}' giving precedence to RCPT TO XORAR={1}", recipient.Email.ToString(), routingAddress2.ToString());
				}
				mdbef.Remove(201916674U);
			}
			if (mdbef.TryGetValue(2883842U, out obj))
			{
				string originalAddressString;
				RedirectionHistoryReason redirectionHistoryReason;
				DateTime dateTime;
				if (string.IsNullOrEmpty(recipient.ORcpt) && RedirectionHistory.TryDecodeRedirectionHistory((byte[])obj, out originalAddressString, out redirectionHistoryReason, out dateTime))
				{
					RedirectionHistory.SetORcpt(recipient, originalAddressString);
				}
				mdbef.Remove(2883842U);
			}
			if (mdbef.Count > 0)
			{
				recipient.ExtendedProperties.SetValue<byte[]>("Microsoft.Exchange.Legacy.PassThru", mdbef.GetBytes());
			}
			return result;
		}

		// Token: 0x06003297 RID: 12951 RVA: 0x000C68DC File Offset: 0x000C4ADC
		private bool TryDecodePFReplica(byte[] rawReplicaDn, out string replicaServerFqdn)
		{
			replicaServerFqdn = null;
			string text = null;
			int num = Array.IndexOf<byte>(rawReplicaDn, 0);
			if (num > 0)
			{
				text = Encoding.ASCII.GetString(rawReplicaDn, 0, num);
				if (text.EndsWith("/cn=Microsoft Public MDB", StringComparison.OrdinalIgnoreCase))
				{
					string serverLegacyDN = text.Substring(0, text.Length - "/cn=Microsoft Public MDB".Length);
					if (this.mailRouter.TryGetServerFqdnByLegacyDN(serverLegacyDN, out replicaServerFqdn))
					{
						ExTraceGlobals.SmtpReceiveTracer.TraceDebug<string, string>(0L, "Translated PF Replica DN '{0}' to server FQDN '{1}'", text, replicaServerFqdn);
					}
				}
			}
			if (replicaServerFqdn == null)
			{
				ExTraceGlobals.SmtpReceiveTracer.TraceError<string>(0L, "Failed to translate PF Replica DN '{0}' to server FQDN", text ?? "<<empty>>");
			}
			return replicaServerFqdn != null;
		}

		// Token: 0x040019A1 RID: 6561
		private TransportMailItem mailItem;

		// Token: 0x040019A2 RID: 6562
		private ISmtpInServer server;

		// Token: 0x040019A3 RID: 6563
		private IMailRouter mailRouter;

		// Token: 0x040019A4 RID: 6564
		private ITransportAppConfig transportAppConfig;

		// Token: 0x040019A5 RID: 6565
		private int? scl;

		// Token: 0x040019A6 RID: 6566
		private SenderIdStatus? senderIdStatus;

		// Token: 0x040019A7 RID: 6567
		private AutoResponseSuppress? suppress;

		// Token: 0x040019A8 RID: 6568
		private DateTime? arrivalTime;

		// Token: 0x040019A9 RID: 6569
		private string messageClass;

		// Token: 0x040019AA RID: 6570
		private bool suppressRecallReport;

		// Token: 0x040019AB RID: 6571
		private bool dlExpansionProhibited;

		// Token: 0x040019AC RID: 6572
		private bool altRecipientProhibited;

		// Token: 0x040019AD RID: 6573
		private BifInfo bifInfo;

		// Token: 0x02000449 RID: 1097
		private class RecipientInternalTranPropFilter
		{
			// Token: 0x06003298 RID: 12952 RVA: 0x000C6979 File Offset: 0x000C4B79
			public RecipientInternalTranPropFilter(MailRecipient recipient)
			{
				this.recipient = recipient;
			}

			// Token: 0x06003299 RID: 12953 RVA: 0x000C6988 File Offset: 0x000C4B88
			public byte[] Filter(ExchangeMailMsgProperty id, byte[] value)
			{
				if (id == ExchangeMailMsgProperty.IMMPID_ERP_ALTERNATE_RECIPIENT_EXPANDED)
				{
					try
					{
						this.recipient.ExtendedProperties.SetValue<bool>("Microsoft.Exchange.Transport.Legacy.AlreadyForwarded", TransportProperty.GetBool(value));
					}
					catch (TransportPropertyException)
					{
					}
					return null;
				}
				return value;
			}

			// Token: 0x040019AE RID: 6574
			private MailRecipient recipient;
		}
	}
}
