using System;
using System.Globalization;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;

namespace Microsoft.Exchange.AirSync.SchemaConverter.AirSync
{
	// Token: 0x0200015D RID: 349
	[Serializable]
	internal class AirSyncReminderProperty : AirSyncIntegerProperty
	{
		// Token: 0x06001008 RID: 4104 RVA: 0x0005B25F File Offset: 0x0005945F
		public AirSyncReminderProperty(string xmlNodeNamespace, string airSyncTagName, bool requiresClientSupport) : base(xmlNodeNamespace, airSyncTagName, requiresClientSupport)
		{
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x0005B26C File Offset: 0x0005946C
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
			int integerData = integerProperty.IntegerData;
			if (-1 != integerData)
			{
				base.CreateAirSyncNode(integerData.ToString(CultureInfo.InvariantCulture));
			}
		}
	}
}
