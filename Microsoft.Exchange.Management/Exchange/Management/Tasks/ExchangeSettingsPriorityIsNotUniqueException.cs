using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200118A RID: 4490
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExchangeSettingsPriorityIsNotUniqueException : ExchangeSettingsException
	{
		// Token: 0x0600B6A2 RID: 46754 RVA: 0x002A02BF File Offset: 0x0029E4BF
		public ExchangeSettingsPriorityIsNotUniqueException(string name, int priority) : base(Strings.ExchangeSettingsPriorityIsNotUnique(name, priority))
		{
			this.name = name;
			this.priority = priority;
		}

		// Token: 0x0600B6A3 RID: 46755 RVA: 0x002A02DC File Offset: 0x0029E4DC
		public ExchangeSettingsPriorityIsNotUniqueException(string name, int priority, Exception innerException) : base(Strings.ExchangeSettingsPriorityIsNotUnique(name, priority), innerException)
		{
			this.name = name;
			this.priority = priority;
		}

		// Token: 0x0600B6A4 RID: 46756 RVA: 0x002A02FC File Offset: 0x0029E4FC
		protected ExchangeSettingsPriorityIsNotUniqueException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
			this.priority = (int)info.GetValue("priority", typeof(int));
		}

		// Token: 0x0600B6A5 RID: 46757 RVA: 0x002A0351 File Offset: 0x0029E551
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
			info.AddValue("priority", this.priority);
		}

		// Token: 0x17003997 RID: 14743
		// (get) Token: 0x0600B6A6 RID: 46758 RVA: 0x002A037D File Offset: 0x0029E57D
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17003998 RID: 14744
		// (get) Token: 0x0600B6A7 RID: 46759 RVA: 0x002A0385 File Offset: 0x0029E585
		public int Priority
		{
			get
			{
				return this.priority;
			}
		}

		// Token: 0x040062FD RID: 25341
		private readonly string name;

		// Token: 0x040062FE RID: 25342
		private readonly int priority;
	}
}
