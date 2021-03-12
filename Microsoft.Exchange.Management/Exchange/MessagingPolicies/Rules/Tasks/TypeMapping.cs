using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B6E RID: 2926
	internal class TypeMapping
	{
		// Token: 0x06006CBB RID: 27835 RVA: 0x001BD1D5 File Offset: 0x001BB3D5
		internal TypeMapping(string name, Type type, LocalizedString linkedDisplayText)
		{
			this.name = name;
			this.type = type;
			this.linkedDisplayText = linkedDisplayText;
		}

		// Token: 0x06006CBC RID: 27836 RVA: 0x001BD1F2 File Offset: 0x001BB3F2
		internal TypeMapping(string name, Type type, LocalizedString linkedDisplayText, LocalizedString linkedDisplayTextException)
		{
			this.name = name;
			this.type = type;
			this.linkedDisplayText = linkedDisplayText;
			this.linkedDisplayTextException = linkedDisplayTextException;
		}

		// Token: 0x170021FA RID: 8698
		// (get) Token: 0x06006CBD RID: 27837 RVA: 0x001BD217 File Offset: 0x001BB417
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170021FB RID: 8699
		// (get) Token: 0x06006CBE RID: 27838 RVA: 0x001BD21F File Offset: 0x001BB41F
		internal Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170021FC RID: 8700
		// (get) Token: 0x06006CBF RID: 27839 RVA: 0x001BD227 File Offset: 0x001BB427
		internal LocalizedString LinkedDisplayText
		{
			get
			{
				return this.linkedDisplayText;
			}
		}

		// Token: 0x170021FD RID: 8701
		// (get) Token: 0x06006CC0 RID: 27840 RVA: 0x001BD22F File Offset: 0x001BB42F
		internal LocalizedString LinkedDisplayTextException
		{
			get
			{
				return this.linkedDisplayTextException;
			}
		}

		// Token: 0x0400386C RID: 14444
		private readonly string name;

		// Token: 0x0400386D RID: 14445
		private Type type;

		// Token: 0x0400386E RID: 14446
		private LocalizedString linkedDisplayText;

		// Token: 0x0400386F RID: 14447
		private LocalizedString linkedDisplayTextException;
	}
}
