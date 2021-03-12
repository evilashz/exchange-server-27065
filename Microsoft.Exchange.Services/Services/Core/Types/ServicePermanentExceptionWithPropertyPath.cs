using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006EF RID: 1775
	internal abstract class ServicePermanentExceptionWithPropertyPath : ServicePermanentException, IProvidePropertyPaths
	{
		// Token: 0x0600361A RID: 13850 RVA: 0x000C1D86 File Offset: 0x000BFF86
		public ServicePermanentExceptionWithPropertyPath(ResponseCodeType responseCode, Enum messageId, Exception innerException) : base(responseCode, messageId, innerException)
		{
		}

		// Token: 0x0600361B RID: 13851 RVA: 0x000C1D9C File Offset: 0x000BFF9C
		public ServicePermanentExceptionWithPropertyPath(ResponseCodeType responseCode, Enum messageId) : base(responseCode, messageId)
		{
		}

		// Token: 0x0600361C RID: 13852 RVA: 0x000C1DB1 File Offset: 0x000BFFB1
		public ServicePermanentExceptionWithPropertyPath(ResponseCodeType responseCode, LocalizedString message, PropertyPath propertyPath) : base(responseCode, message)
		{
			this.propertyPaths.Add(propertyPath);
		}

		// Token: 0x0600361D RID: 13853 RVA: 0x000C1DD2 File Offset: 0x000BFFD2
		public ServicePermanentExceptionWithPropertyPath(ResponseCodeType responseCode, Enum messageId, PropertyPath propertyPath) : base(responseCode, messageId)
		{
			this.propertyPaths.Add(propertyPath);
		}

		// Token: 0x0600361E RID: 13854 RVA: 0x000C1DF3 File Offset: 0x000BFFF3
		public ServicePermanentExceptionWithPropertyPath(Enum messageId, PropertyPath propertyPath) : base(messageId)
		{
			this.propertyPaths.Add(propertyPath);
		}

		// Token: 0x0600361F RID: 13855 RVA: 0x000C1E13 File Offset: 0x000C0013
		public ServicePermanentExceptionWithPropertyPath(Enum messageId, PropertyPath propertyPath, Exception innerException) : base(messageId, innerException)
		{
			this.propertyPaths.Add(propertyPath);
		}

		// Token: 0x06003620 RID: 13856 RVA: 0x000C1E34 File Offset: 0x000C0034
		public ServicePermanentExceptionWithPropertyPath(ResponseCodeType responseCode, Enum messageId, PropertyPath propertyPath, Exception innerException) : base(responseCode, messageId, innerException)
		{
			this.propertyPaths.Add(propertyPath);
		}

		// Token: 0x06003621 RID: 13857 RVA: 0x000C1E58 File Offset: 0x000C0058
		public ServicePermanentExceptionWithPropertyPath(Enum messageId, PropertyPath[] propertyPaths, Exception innerException) : base(messageId, innerException)
		{
			foreach (PropertyPath item in propertyPaths)
			{
				this.propertyPaths.Add(item);
			}
		}

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x06003622 RID: 13858 RVA: 0x000C1E98 File Offset: 0x000C0098
		PropertyPath[] IProvidePropertyPaths.PropertyPaths
		{
			get
			{
				return this.propertyPaths.ToArray();
			}
		}

		// Token: 0x06003623 RID: 13859 RVA: 0x000C1EA8 File Offset: 0x000C00A8
		string IProvidePropertyPaths.GetPropertyPathsString()
		{
			if (this.propertyPaths == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			foreach (PropertyPath arg in this.propertyPaths)
			{
				if (num == this.propertyPaths.Count - 1)
				{
					stringBuilder.AppendFormat("'{0}'", arg);
				}
				else
				{
					stringBuilder.AppendFormat("'{0}', ", arg);
				}
				num++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001E3B RID: 7739
		private List<PropertyPath> propertyPaths = new List<PropertyPath>();
	}
}
