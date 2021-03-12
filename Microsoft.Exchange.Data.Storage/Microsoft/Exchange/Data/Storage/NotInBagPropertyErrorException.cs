using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000758 RID: 1880
	[Serializable]
	public class NotInBagPropertyErrorException : ExInvalidOperationException
	{
		// Token: 0x06004859 RID: 18521 RVA: 0x00130E47 File Offset: 0x0012F047
		public NotInBagPropertyErrorException(PropertyDefinition propertyDefinition) : base(new LocalizedString(propertyDefinition.ToString()))
		{
			this.propertyDefinition = propertyDefinition;
		}

		// Token: 0x0600485A RID: 18522 RVA: 0x00130E66 File Offset: 0x0012F066
		protected NotInBagPropertyErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.propertyDefinition = (PropertyDefinition)info.GetValue("propertyDefinition", typeof(PropertyDefinition));
		}

		// Token: 0x0600485B RID: 18523 RVA: 0x00130E90 File Offset: 0x0012F090
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("propertyDefinition", this.propertyDefinition);
		}

		// Token: 0x0400274D RID: 10061
		private const string PropertyDefinitionLabel = "propertyDefinition";

		// Token: 0x0400274E RID: 10062
		private readonly PropertyDefinition propertyDefinition;
	}
}
