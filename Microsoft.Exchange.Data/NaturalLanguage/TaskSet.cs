using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.NaturalLanguage
{
	// Token: 0x0200007F RID: 127
	public class TaskSet : ExtractionSet<Task>
	{
		// Token: 0x060003C7 RID: 967 RVA: 0x0000E18B File Offset: 0x0000C38B
		public TaskSet() : base(new TaskSerializer())
		{
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000E198 File Offset: 0x0000C398
		public static implicit operator TaskSet(Task[] tasks)
		{
			return new TaskSet
			{
				Extractions = tasks
			};
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0000E1B3 File Offset: 0x0000C3B3
		public static XmlSerializer Serializer
		{
			get
			{
				return TaskSet.serializer;
			}
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000E1BA File Offset: 0x0000C3BA
		public override XmlSerializer GetSerializer()
		{
			return TaskSet.serializer;
		}

		// Token: 0x0400018F RID: 399
		private static XmlSerializer serializer = new XmlSerializer(typeof(TaskSet));
	}
}
