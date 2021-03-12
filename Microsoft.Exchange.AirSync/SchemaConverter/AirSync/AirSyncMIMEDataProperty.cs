using System;
using System.Globalization;
using System.IO;
using System.Xml;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;

namespace Microsoft.Exchange.AirSync.SchemaConverter.AirSync
{
	// Token: 0x02000159 RID: 345
	[Serializable]
	internal class AirSyncMIMEDataProperty : AirSyncProperty, IMIMEDataProperty, IMIMERelatedProperty, IProperty
	{
		// Token: 0x06000FFB RID: 4091 RVA: 0x0005AFDD File Offset: 0x000591DD
		public AirSyncMIMEDataProperty(string xmlNodeNamespace, bool requiresClientSupport) : base(xmlNodeNamespace, "MIMEData", requiresClientSupport)
		{
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06000FFC RID: 4092 RVA: 0x0005AFEC File Offset: 0x000591EC
		public bool IsOnSMIMEMessage
		{
			get
			{
				throw new NotImplementedException("IsOnSMIMEMessage should not be called");
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06000FFD RID: 4093 RVA: 0x0005AFF8 File Offset: 0x000591F8
		// (set) Token: 0x06000FFE RID: 4094 RVA: 0x0005B000 File Offset: 0x00059200
		public Stream MIMEData { get; set; }

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06000FFF RID: 4095 RVA: 0x0005B009 File Offset: 0x00059209
		// (set) Token: 0x06001000 RID: 4096 RVA: 0x0005B011 File Offset: 0x00059211
		public long MIMESize { get; set; }

		// Token: 0x06001001 RID: 4097 RVA: 0x0005B01C File Offset: 0x0005921C
		protected override void InternalCopyFrom(IProperty srcProperty)
		{
			IMIMEDataProperty imimedataProperty = srcProperty as IMIMEDataProperty;
			if (imimedataProperty == null)
			{
				throw new UnexpectedTypeException("IMIMEDataProperty", srcProperty);
			}
			if (!BodyUtility.IsAskingForMIMEData(imimedataProperty, base.Options))
			{
				return;
			}
			int num = -1;
			bool flag = false;
			imimedataProperty.MIMESize = imimedataProperty.MIMEData.Length;
			if (base.Options.Contains("MIMETruncation"))
			{
				num = (int)base.Options["MIMETruncation"];
			}
			if (num >= 0 && (long)num < imimedataProperty.MIMEData.Length)
			{
				flag = true;
				imimedataProperty.MIMESize = (long)num;
			}
			base.CreateAirSyncNode("MIMETruncated", flag ? "1" : "0");
			if (flag)
			{
				imimedataProperty.MIMESize = (long)num;
				base.CreateAirSyncNode("MIMESize", imimedataProperty.MIMEData.Length.ToString(CultureInfo.InvariantCulture));
			}
			base.CreateAirSyncNode("MIMEData", imimedataProperty.MIMEData, imimedataProperty.MIMESize, XmlNodeType.Text);
		}
	}
}
