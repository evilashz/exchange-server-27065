using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020002BA RID: 698
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ConditionInitializationException : LocalizedException
	{
		// Token: 0x06001915 RID: 6421 RVA: 0x0005CBB5 File Offset: 0x0005ADB5
		public ConditionInitializationException(string uninitializedProperty, Condition owningCondition) : base(Strings.ConditionNotInitialized(uninitializedProperty, owningCondition))
		{
			this.uninitializedProperty = uninitializedProperty;
			this.owningCondition = owningCondition;
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x0005CBD2 File Offset: 0x0005ADD2
		public ConditionInitializationException(string uninitializedProperty, Condition owningCondition, Exception innerException) : base(Strings.ConditionNotInitialized(uninitializedProperty, owningCondition), innerException)
		{
			this.uninitializedProperty = uninitializedProperty;
			this.owningCondition = owningCondition;
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x0005CBF0 File Offset: 0x0005ADF0
		protected ConditionInitializationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.uninitializedProperty = (string)info.GetValue("uninitializedProperty", typeof(string));
			this.owningCondition = (Condition)info.GetValue("owningCondition", typeof(Condition));
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x0005CC45 File Offset: 0x0005AE45
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("uninitializedProperty", this.uninitializedProperty);
			info.AddValue("owningCondition", this.owningCondition);
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06001919 RID: 6425 RVA: 0x0005CC71 File Offset: 0x0005AE71
		public string UninitializedProperty
		{
			get
			{
				return this.uninitializedProperty;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x0600191A RID: 6426 RVA: 0x0005CC79 File Offset: 0x0005AE79
		public Condition OwningCondition
		{
			get
			{
				return this.owningCondition;
			}
		}

		// Token: 0x04000992 RID: 2450
		private readonly string uninitializedProperty;

		// Token: 0x04000993 RID: 2451
		private readonly Condition owningCondition;
	}
}
