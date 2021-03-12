using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004CC RID: 1228
	[DataContract]
	public class SetUMMailboxParameters : SetObjectProperties
	{
		// Token: 0x06003C41 RID: 15425 RVA: 0x000B5150 File Offset: 0x000B3350
		public SetUMMailboxParameters()
		{
			this.OnDeserializing(default(StreamingContext));
		}

		// Token: 0x06003C42 RID: 15426 RVA: 0x000B5172 File Offset: 0x000B3372
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			this.SetUMExtensionParameteres = new SetUMExtensionParameteres();
		}

		// Token: 0x170023C5 RID: 9157
		// (get) Token: 0x06003C43 RID: 15427 RVA: 0x000B517F File Offset: 0x000B337F
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-UMMailbox";
			}
		}

		// Token: 0x170023C6 RID: 9158
		// (get) Token: 0x06003C44 RID: 15428 RVA: 0x000B5186 File Offset: 0x000B3386
		public override string RbacScope
		{
			get
			{
				return "@W:Organization";
			}
		}

		// Token: 0x170023C7 RID: 9159
		// (get) Token: 0x06003C45 RID: 15429 RVA: 0x000B518D File Offset: 0x000B338D
		// (set) Token: 0x06003C46 RID: 15430 RVA: 0x000B519F File Offset: 0x000B339F
		[DataMember]
		public Identity Identity
		{
			get
			{
				return (Identity)base["Identity"];
			}
			set
			{
				base["Identity"] = value;
			}
		}

		// Token: 0x170023C8 RID: 9160
		// (get) Token: 0x06003C47 RID: 15431 RVA: 0x000B51AD File Offset: 0x000B33AD
		// (set) Token: 0x06003C48 RID: 15432 RVA: 0x000B51BF File Offset: 0x000B33BF
		[DataMember]
		public Identity UMMailboxPolicy
		{
			get
			{
				return (Identity)base[UMMailboxSchema.UMMailboxPolicy];
			}
			set
			{
				value.FaultIfNull(Strings.UMMailboxPolicyErrorMessage);
				base[UMMailboxSchema.UMMailboxPolicy] = value;
			}
		}

		// Token: 0x170023C9 RID: 9161
		// (get) Token: 0x06003C49 RID: 15433 RVA: 0x000B51DD File Offset: 0x000B33DD
		// (set) Token: 0x06003C4A RID: 15434 RVA: 0x000B51EF File Offset: 0x000B33EF
		[DataMember]
		public string OperatorNumber
		{
			get
			{
				return (string)base[UMMailboxSchema.OperatorNumber];
			}
			set
			{
				base[UMMailboxSchema.OperatorNumber] = value;
			}
		}

		// Token: 0x170023CA RID: 9162
		// (get) Token: 0x06003C4B RID: 15435 RVA: 0x000B51FD File Offset: 0x000B33FD
		// (set) Token: 0x06003C4C RID: 15436 RVA: 0x000B5205 File Offset: 0x000B3405
		public SetUMExtensionParameteres SetUMExtensionParameteres { get; private set; }

		// Token: 0x170023CB RID: 9163
		// (get) Token: 0x06003C4D RID: 15437 RVA: 0x000B520E File Offset: 0x000B340E
		// (set) Token: 0x06003C4E RID: 15438 RVA: 0x000B521B File Offset: 0x000B341B
		[DataMember]
		public IEnumerable<UMExtension> SecondaryExtensions
		{
			get
			{
				return this.SetUMExtensionParameteres.SecondaryExtensions;
			}
			set
			{
				this.SetUMExtensionParameteres.SecondaryExtensions = value;
			}
		}
	}
}
