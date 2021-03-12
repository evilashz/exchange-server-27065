using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x0200001A RID: 26
	internal abstract class MobileServiceCapability
	{
		// Token: 0x0600008C RID: 140 RVA: 0x00004130 File Offset: 0x00002330
		public static bool DoesCodingAndCapacityMatch(MobileServiceCapability a, MobileServiceCapability b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (a.CodingSupportabilities.Count != b.CodingSupportabilities.Count)
			{
				return false;
			}
			if (a.SupportedPartType != b.SupportedPartType)
			{
				return false;
			}
			bool flag = (PartType)0 != (a.SupportedPartType | PartType.Concatenated);
			if (flag && a.SegmentsPerConcatenatedPart != b.SegmentsPerConcatenatedPart)
			{
				return false;
			}
			SmtpToSmsGatewayCapability smtpToSmsGatewayCapability = a as SmtpToSmsGatewayCapability;
			SmtpToSmsGatewayCapability smtpToSmsGatewayCapability2 = b as SmtpToSmsGatewayCapability;
			bool flag2 = smtpToSmsGatewayCapability != null && smtpToSmsGatewayCapability.Parameters != null && null != smtpToSmsGatewayCapability.Parameters.MessageRendering;
			bool flag3 = smtpToSmsGatewayCapability2 != null && smtpToSmsGatewayCapability2.Parameters != null && null != smtpToSmsGatewayCapability2.Parameters.MessageRendering;
			if (flag2 != flag3)
			{
				return false;
			}
			if (smtpToSmsGatewayCapability.Parameters.MessageRendering.Container != smtpToSmsGatewayCapability2.Parameters.MessageRendering.Container)
			{
				return false;
			}
			foreach (CodingSupportability codingSupportability in a.CodingSupportabilities)
			{
				bool flag4 = false;
				foreach (CodingSupportability codingSupportability2 in b.CodingSupportabilities)
				{
					if (codingSupportability.CodingScheme == codingSupportability2.CodingScheme && codingSupportability.RadixPerPart == codingSupportability2.RadixPerPart && (!flag || codingSupportability.RadixPerSegment == codingSupportability2.RadixPerSegment))
					{
						flag4 = true;
						break;
					}
				}
				if (!flag4)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000042D8 File Offset: 0x000024D8
		internal MobileServiceCapability(PartType supportedPartType, int segmentsPerConcatenatedPart, IList<CodingSupportability> codingSupportabilities, FeatureSupportability featureSupportabilities)
		{
			this.SupportedPartType = supportedPartType;
			this.SegmentsPerConcatenatedPart = segmentsPerConcatenatedPart;
			if (!codingSupportabilities.IsReadOnly)
			{
				codingSupportabilities = new ReadOnlyCollection<CodingSupportability>(codingSupportabilities);
			}
			this.CodingSupportabilities = codingSupportabilities;
			this.FeatureSupportabilities = featureSupportabilities;
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600008E RID: 142 RVA: 0x0000430D File Offset: 0x0000250D
		// (set) Token: 0x0600008F RID: 143 RVA: 0x00004315 File Offset: 0x00002515
		public PartType SupportedPartType { get; private set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000090 RID: 144 RVA: 0x0000431E File Offset: 0x0000251E
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00004326 File Offset: 0x00002526
		public int SegmentsPerConcatenatedPart { get; private set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000092 RID: 146 RVA: 0x0000432F File Offset: 0x0000252F
		// (set) Token: 0x06000093 RID: 147 RVA: 0x00004337 File Offset: 0x00002537
		public IList<CodingSupportability> CodingSupportabilities { get; private set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00004340 File Offset: 0x00002540
		// (set) Token: 0x06000095 RID: 149 RVA: 0x00004348 File Offset: 0x00002548
		public FeatureSupportability FeatureSupportabilities { get; private set; }
	}
}
