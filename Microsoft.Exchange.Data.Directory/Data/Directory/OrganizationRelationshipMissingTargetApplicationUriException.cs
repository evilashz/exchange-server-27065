using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AE5 RID: 2789
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class OrganizationRelationshipMissingTargetApplicationUriException : ADOperationException
	{
		// Token: 0x0600812D RID: 33069 RVA: 0x001A63F7 File Offset: 0x001A45F7
		public OrganizationRelationshipMissingTargetApplicationUriException() : base(DirectoryStrings.OrganizationRelationshipMissingTargetApplicationUri)
		{
		}

		// Token: 0x0600812E RID: 33070 RVA: 0x001A6404 File Offset: 0x001A4604
		public OrganizationRelationshipMissingTargetApplicationUriException(Exception innerException) : base(DirectoryStrings.OrganizationRelationshipMissingTargetApplicationUri, innerException)
		{
		}

		// Token: 0x0600812F RID: 33071 RVA: 0x001A6412 File Offset: 0x001A4612
		protected OrganizationRelationshipMissingTargetApplicationUriException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06008130 RID: 33072 RVA: 0x001A641C File Offset: 0x001A461C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
