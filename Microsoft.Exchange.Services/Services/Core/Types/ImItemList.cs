using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005E2 RID: 1506
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "ImItemList")]
	[XmlType("ImItemListType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ImItemList
	{
		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06002D5A RID: 11610 RVA: 0x000B20BE File Offset: 0x000B02BE
		// (set) Token: 0x06002D5B RID: 11611 RVA: 0x000B20C6 File Offset: 0x000B02C6
		[XmlArray]
		[DataMember(Order = 1)]
		[XmlArrayItem("ImGroup", typeof(ImGroup))]
		public ImGroup[] Groups { get; set; }

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06002D5C RID: 11612 RVA: 0x000B20CF File Offset: 0x000B02CF
		// (set) Token: 0x06002D5D RID: 11613 RVA: 0x000B20D7 File Offset: 0x000B02D7
		[XmlArray]
		[XmlArrayItem("Persona", typeof(Persona))]
		[DataMember(Order = 2)]
		public Persona[] Personas { get; set; }

		// Token: 0x06002D5E RID: 11614 RVA: 0x000B20E0 File Offset: 0x000B02E0
		internal static ImItemList LoadFrom(RawImItemList rawImItemList, ExtendedPropertyUri[] extendedProperties, MailboxSession session)
		{
			ImGroup[] array = null;
			if (rawImItemList.Groups.Length > 0)
			{
				array = new ImGroup[rawImItemList.Groups.Length];
				for (int i = 0; i < rawImItemList.Groups.Length; i++)
				{
					array[i] = ImGroup.LoadFromRawImGroup(rawImItemList.Groups[i], session);
				}
			}
			Persona[] array2 = null;
			int num = 0;
			if (rawImItemList.Personas.Length > 0)
			{
				PropertyDefinition[] array3 = Array<PropertyDefinition>.Empty;
				if (extendedProperties != null)
				{
					array3 = new PropertyDefinition[extendedProperties.Length];
					for (int j = 0; j < extendedProperties.Length; j++)
					{
						array3[j] = extendedProperties[j].ToPropertyDefinition();
					}
				}
				array2 = new Persona[rawImItemList.Personas.Length];
				for (int k = 0; k < rawImItemList.Personas.Length; k++)
				{
					PersonId personId = rawImItemList.Personas[k];
					ItemId personaId = IdConverter.PersonaIdFromPersonId(session.MailboxOwner.MailboxInfo.MailboxGuid, personId);
					array2[k] = Persona.LoadFromPersonaId(session, null, personaId, Persona.FullPersonaShape, array3, null);
					num += array2[k].Attributions.Length;
				}
			}
			if (array != null)
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericInfo(RequestDetailsLogger.Current, "ImGroupCount", array.Length);
			}
			else
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericInfo(RequestDetailsLogger.Current, "ImGroupCount", 0);
			}
			if (array2 != null)
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericInfo(RequestDetailsLogger.Current, "PersonaCount", array2.Length);
			}
			else
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericInfo(RequestDetailsLogger.Current, "PersonaCount", 0);
			}
			RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericInfo(RequestDetailsLogger.Current, "ContactCount", num);
			return new ImItemList
			{
				Groups = array,
				Personas = array2
			};
		}
	}
}
