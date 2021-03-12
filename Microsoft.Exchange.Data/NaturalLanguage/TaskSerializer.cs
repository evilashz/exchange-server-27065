using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.NaturalLanguage
{
	// Token: 0x02000075 RID: 117
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class TaskSerializer : BaseSerializer<Task>
	{
		// Token: 0x060003A3 RID: 931 RVA: 0x0000DEF3 File Offset: 0x0000C0F3
		protected override XmlSerializer GetSerializer()
		{
			return TaskSerializer.serializer;
		}

		// Token: 0x04000183 RID: 387
		private static XmlSerializer serializer = new XmlSerializer(typeof(Task[]), new XmlRootAttribute("Tasks"));
	}
}
