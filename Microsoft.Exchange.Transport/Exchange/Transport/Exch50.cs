using System;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000443 RID: 1091
	internal class Exch50
	{
		// Token: 0x06003275 RID: 12917 RVA: 0x000C5984 File Offset: 0x000C3B84
		protected static byte[] FilterTransportProperties(byte[] data, Exch50.TransportPropertyFilter filter)
		{
			TransportPropertyReader transportPropertyReader = new TransportPropertyReader(data, 0, data.Length);
			TransportPropertyWriter transportPropertyWriter = new TransportPropertyWriter();
			while (transportPropertyReader.ReadNextProperty())
			{
				byte[] array = transportPropertyReader.Value;
				if (transportPropertyReader.Range == Exch50.ExchangeMailMsgPropertyRange)
				{
					array = filter((ExchangeMailMsgProperty)transportPropertyReader.Id, array);
				}
				if (array != null)
				{
					transportPropertyWriter.Add(transportPropertyReader.Range, transportPropertyReader.Id, transportPropertyReader.Value);
				}
			}
			if (transportPropertyWriter.Length == 0)
			{
				return null;
			}
			return transportPropertyWriter.GetBytes();
		}

		// Token: 0x06003276 RID: 12918 RVA: 0x000C5A00 File Offset: 0x000C3C00
		protected static void CopyJournalProperties(byte[] data, TransportMailItem mailItem)
		{
			TransportPropertyReader transportPropertyReader = new TransportPropertyReader(data, 0, data.Length);
			LegacyJournalReportType legacyJournalReportType = LegacyJournalReportType.Default;
			while (transportPropertyReader.ReadNextProperty())
			{
				byte[] value = transportPropertyReader.Value;
				ExchangeMailMsgProperty id = (ExchangeMailMsgProperty)transportPropertyReader.Id;
				if (id != ExchangeMailMsgProperty.IMMPID_EMP_ORIGINAL_P1_RECIPIENT_LIST)
				{
					switch (id)
					{
					case ExchangeMailMsgProperty.IMMPID_EMP_EJ_RECIPIENT_LIST:
						mailItem.ExtendedProperties.SetValue<byte[]>("Microsoft.Exchange.EnvelopeJournalRecipientList", value);
						if (legacyJournalReportType != LegacyJournalReportType.EnvelopeV2)
						{
							legacyJournalReportType = LegacyJournalReportType.Envelope;
						}
						break;
					case ExchangeMailMsgProperty.IMMPID_EMP_EJ_RECIPIENT_P2_TYPE_LIST:
						legacyJournalReportType = LegacyJournalReportType.EnvelopeV2;
						mailItem.ExtendedProperties.SetValue<byte[]>("Microsoft.Exchange.EnvelopeJournalRecipientType", value);
						break;
					case ExchangeMailMsgProperty.IMMPID_EMP_EJ_EXPANSION_HISTORY_LIST:
						legacyJournalReportType = LegacyJournalReportType.EnvelopeV2;
						mailItem.ExtendedProperties.SetValue<byte[]>("Microsoft.Exchange.EnvelopeJournalExpansionHistory", value);
						break;
					}
				}
				else
				{
					mailItem.ExtendedProperties.SetValue<byte[]>("Microsoft.Exchange.OriginalP1RecipientList", value);
					legacyJournalReportType = LegacyJournalReportType.Bcc;
				}
			}
			mailItem.ExtendedProperties.SetValue<int>("Microsoft.Exchange.LegacyJournalReport", (int)legacyJournalReportType);
		}

		// Token: 0x04001994 RID: 6548
		public const string ContentIdentifierProperty = "Microsoft.Exchange.ContentIdentifier";

		// Token: 0x04001995 RID: 6549
		public const string RecipientMdbefPassThru = "Microsoft.Exchange.Legacy.PassThru";

		// Token: 0x04001996 RID: 6550
		public const string ExJournalData = "EXJournalData";

		// Token: 0x04001997 RID: 6551
		public const string JournalRecipientList = "Microsoft.Exchange.JournalRecipientList";

		// Token: 0x04001998 RID: 6552
		protected const string PublicMdbSuffix = "/cn=Microsoft Public MDB";

		// Token: 0x04001999 RID: 6553
		protected static readonly Guid ExchangeMailMsgPropertyRange = new Guid(4233967246U, 33985, 4562, 155, 237, 0, 160, 201, 94, 97, 67);

		// Token: 0x02000444 RID: 1092
		// (Invoke) Token: 0x0600327A RID: 12922
		protected delegate byte[] TransportPropertyFilter(ExchangeMailMsgProperty id, byte[] value);
	}
}
