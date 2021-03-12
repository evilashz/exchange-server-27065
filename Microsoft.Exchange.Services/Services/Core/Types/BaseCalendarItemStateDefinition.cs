using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006E1 RID: 1761
	[XmlInclude(typeof(DeleteFromFolderStateDefinition))]
	[XmlType(TypeName = "BaseCalendarItemStateDefinitionType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlInclude(typeof(DeletedOccurrenceStateDefinition))]
	[XmlInclude(typeof(LocationBasedStateDefinition))]
	[Serializable]
	public class BaseCalendarItemStateDefinition
	{
	}
}
