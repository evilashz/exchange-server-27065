using System;
using System.Globalization;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;

namespace Microsoft.Exchange.AirSync.SchemaConverter.AirSync
{
	// Token: 0x02000156 RID: 342
	[Serializable]
	internal class AirSyncIntegerProperty : AirSyncProperty, IIntegerProperty, IProperty
	{
		// Token: 0x06000FF1 RID: 4081 RVA: 0x0005AC81 File Offset: 0x00058E81
		public AirSyncIntegerProperty(string xmlNodeNamespace, string airSyncTagName, bool requiresClientSupport) : base(xmlNodeNamespace, airSyncTagName, requiresClientSupport)
		{
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06000FF2 RID: 4082 RVA: 0x0005AC8C File Offset: 0x00058E8C
		public virtual int IntegerData
		{
			get
			{
				return Convert.ToInt32(base.XmlNode.InnerText, CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x0005ACA4 File Offset: 0x00058EA4
		protected override void InternalCopyFrom(IProperty srcProperty)
		{
			IIntegerProperty integerProperty = srcProperty as IIntegerProperty;
			if (integerProperty == null)
			{
				throw new UnexpectedTypeException("IIntegerProperty", srcProperty);
			}
			if (PropertyState.Modified != srcProperty.State)
			{
				throw new ConversionException("Property only supports conversion from Modified property state");
			}
			base.CreateAirSyncNode(integerProperty.IntegerData.ToString(CultureInfo.InvariantCulture));
		}
	}
}
