using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.SchemaConverter;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x0200023E RID: 574
	[Serializable]
	internal class XsoUidProperty : XsoStringProperty
	{
		// Token: 0x06001528 RID: 5416 RVA: 0x0007BE8A File Offset: 0x0007A08A
		public XsoUidProperty(StorePropertyDefinition propertyDef) : base(propertyDef)
		{
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x0007BE93 File Offset: 0x0007A093
		public XsoUidProperty(StorePropertyDefinition propertyDef, PropertyType type) : base(propertyDef, type)
		{
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x0600152A RID: 5418 RVA: 0x0007BEA0 File Offset: 0x0007A0A0
		public override string StringData
		{
			get
			{
				byte[] array = base.XsoItem.TryGetProperty(base.PropertyDef) as byte[];
				if (array == null)
				{
					AirSyncDiagnostics.Assert(false, "GlobalObjectId property not returned by XSO", new object[0]);
					return string.Empty;
				}
				GlobalObjectId globalObjectId = null;
				try
				{
					globalObjectId = new GlobalObjectId(array);
				}
				catch (CorruptDataException)
				{
					return string.Empty;
				}
				string text = globalObjectId.Uid;
				int num = text.IndexOf('\0');
				if (num != -1)
				{
					text = text.Substring(0, num);
				}
				return text;
			}
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x0007BF24 File Offset: 0x0007A124
		protected override void InternalCopyFromModified(IProperty srcProperty)
		{
			IStringProperty stringProperty = (IStringProperty)srcProperty;
			byte[] array = base.XsoItem.TryGetProperty(base.PropertyDef) as byte[];
			if (array == null)
			{
				array = new GlobalObjectId(stringProperty.StringData).Bytes;
				base.XsoItem[CalendarItemBaseSchema.GlobalObjectId] = array;
				base.XsoItem[CalendarItemBaseSchema.CleanGlobalObjectId] = array;
				return;
			}
			GlobalObjectId globalObjectId = new GlobalObjectId(array);
			if (globalObjectId.Uid != stringProperty.StringData)
			{
				AirSyncDiagnostics.TraceDebug<string, string>(ExTraceGlobals.XsoTracer, this, "Client overwrote Uid from {0} to {1}", globalObjectId.Uid, stringProperty.StringData);
			}
			array = new GlobalObjectId(stringProperty.StringData).Bytes;
			base.XsoItem[CalendarItemBaseSchema.GlobalObjectId] = array;
			base.XsoItem[CalendarItemBaseSchema.CleanGlobalObjectId] = array;
		}
	}
}
