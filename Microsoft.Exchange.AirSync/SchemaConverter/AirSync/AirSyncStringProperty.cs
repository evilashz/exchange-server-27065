using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Diagnostics.Components.SchemaConverter;

namespace Microsoft.Exchange.AirSync.SchemaConverter.AirSync
{
	// Token: 0x0200013D RID: 317
	[Serializable]
	internal class AirSyncStringProperty : AirSyncProperty, IStringProperty, IProperty
	{
		// Token: 0x06000F92 RID: 3986 RVA: 0x000590BC File Offset: 0x000572BC
		public AirSyncStringProperty(string xmlNodeNamespace, string airSyncTagName, bool requiresClientSupport) : base(xmlNodeNamespace, airSyncTagName, requiresClientSupport)
		{
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06000F93 RID: 3987 RVA: 0x000590C7 File Offset: 0x000572C7
		public string StringData
		{
			get
			{
				return base.XmlNode.InnerText;
			}
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x000590D4 File Offset: 0x000572D4
		protected override void InternalCopyFrom(IProperty srcProperty)
		{
			IStringProperty stringProperty = srcProperty as IStringProperty;
			if (stringProperty == null)
			{
				throw new UnexpectedTypeException("IStringProperty", srcProperty, base.AirSyncTagNames[0]);
			}
			string text = stringProperty.StringData;
			bool flag = string.Equals(base.AirSyncTagNames[0], "Location");
			if (PropertyState.Modified == srcProperty.State)
			{
				if (string.IsNullOrEmpty(text) && !flag)
				{
					AirSyncDiagnostics.TraceInfo<string>(ExTraceGlobals.AirSyncTracer, this, "Node={0} String data is null or empty.", base.AirSyncTagNames[0]);
					this.InternalSetToDefault(srcProperty);
					return;
				}
				if (text.Length > 32000)
				{
					text = text.Substring(0, 32000);
				}
				base.CreateAirSyncNode(text, flag);
			}
		}

		// Token: 0x04000A68 RID: 2664
		private const int MaxStringLength = 32000;
	}
}
