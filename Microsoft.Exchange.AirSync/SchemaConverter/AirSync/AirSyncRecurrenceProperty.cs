using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;

namespace Microsoft.Exchange.AirSync.SchemaConverter.AirSync
{
	// Token: 0x0200015C RID: 348
	[Serializable]
	internal class AirSyncRecurrenceProperty : AirSyncNestedProperty
	{
		// Token: 0x06001006 RID: 4102 RVA: 0x0005B165 File Offset: 0x00059365
		public AirSyncRecurrenceProperty(string xmlNodeNamespace, string airSyncTagName, TypeOfRecurrence recurrenceType, bool requiresClientSupport, int protocolVersion) : base(xmlNodeNamespace, airSyncTagName, new RecurrenceData(recurrenceType, protocolVersion), requiresClientSupport)
		{
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x0005B17C File Offset: 0x0005937C
		protected override void InternalCopyFrom(IProperty srcProperty)
		{
			INestedProperty nestedProperty = srcProperty as INestedProperty;
			if (nestedProperty == null)
			{
				throw new UnexpectedTypeException("INestedProperty", srcProperty);
			}
			RecurrenceData recurrenceData = nestedProperty.NestedData as RecurrenceData;
			if (recurrenceData == null)
			{
				throw new UnexpectedTypeException("RecurrenceData", nestedProperty.NestedData);
			}
			if (srcProperty.State != PropertyState.Modified)
			{
				return;
			}
			base.XmlNode = base.XmlParentNode.OwnerDocument.CreateElement(base.AirSyncTagNames[0], base.Namespace);
			for (int i = 0; i < recurrenceData.Keys.Length; i++)
			{
				string text = recurrenceData.SubProperties[recurrenceData.Keys[i]] as string;
				if (text == null && recurrenceData.Keys[i] == "Type")
				{
					throw new ConversionException("Type of Recurrence has to be specified");
				}
				if (text != null)
				{
					base.AppendChildNode(recurrenceData.Keys[i], text);
				}
			}
			base.XmlParentNode.AppendChild(base.XmlNode);
		}
	}
}
