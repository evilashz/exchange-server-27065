using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.HA
{
	// Token: 0x0200002B RID: 43
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MinimizedPropertyException : LocalizedException
	{
		// Token: 0x060001CE RID: 462 RVA: 0x000050F2 File Offset: 0x000032F2
		public MinimizedPropertyException(string propertyName) : base(Strings.MinimizedProperty(propertyName))
		{
			this.propertyName = propertyName;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00005107 File Offset: 0x00003307
		public MinimizedPropertyException(string propertyName, Exception innerException) : base(Strings.MinimizedProperty(propertyName), innerException)
		{
			this.propertyName = propertyName;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000511D File Offset: 0x0000331D
		protected MinimizedPropertyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.propertyName = (string)info.GetValue("propertyName", typeof(string));
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00005147 File Offset: 0x00003347
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("propertyName", this.propertyName);
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00005162 File Offset: 0x00003362
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x04000099 RID: 153
		private readonly string propertyName;
	}
}
