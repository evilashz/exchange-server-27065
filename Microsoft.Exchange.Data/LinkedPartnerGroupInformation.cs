using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200016B RID: 363
	internal class LinkedPartnerGroupInformation : IEquatable<LinkedPartnerGroupInformation>
	{
		// Token: 0x06000C08 RID: 3080 RVA: 0x000255D4 File Offset: 0x000237D4
		internal LinkedPartnerGroupInformation()
		{
			this.linkedPartnerGroupInfoTokens = new string[2];
			this.LinkedPartnerGroupId = string.Empty;
			this.LinkedPartnerOrganizationId = string.Empty;
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x000255FE File Offset: 0x000237FE
		private LinkedPartnerGroupInformation(string linkedPartnerGroupAndOrganizationId)
		{
			if (linkedPartnerGroupAndOrganizationId == null)
			{
				throw new ArgumentNullException("linkedPartnerGroupAndOrganizationId");
			}
			this.linkedPartnerGroupInfoTokens = LinkedPartnerGroupInformation.GetLinkedPartnerIdValues(linkedPartnerGroupAndOrganizationId);
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x00025620 File Offset: 0x00023820
		// (set) Token: 0x06000C0B RID: 3083 RVA: 0x0002562A File Offset: 0x0002382A
		public string LinkedPartnerGroupId
		{
			get
			{
				return this.linkedPartnerGroupInfoTokens[0];
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("LinkedPartnerGroupId");
				}
				this.linkedPartnerGroupInfoTokens[0] = value;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x00025643 File Offset: 0x00023843
		// (set) Token: 0x06000C0D RID: 3085 RVA: 0x0002564D File Offset: 0x0002384D
		public string LinkedPartnerOrganizationId
		{
			get
			{
				return this.linkedPartnerGroupInfoTokens[1];
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("LinkedPartnerOrganizationId");
				}
				this.linkedPartnerGroupInfoTokens[1] = value;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x00025666 File Offset: 0x00023866
		public bool IsValidADObject
		{
			get
			{
				return !string.IsNullOrEmpty(this.LinkedPartnerGroupId) && !string.IsNullOrEmpty(this.LinkedPartnerOrganizationId);
			}
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x00025685 File Offset: 0x00023885
		public static LinkedPartnerGroupInformation Parse(string linkedPartnerAndOrganizationId)
		{
			return new LinkedPartnerGroupInformation(linkedPartnerAndOrganizationId);
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0002568D File Offset: 0x0002388D
		public override string ToString()
		{
			return string.Format("{0}{1}{2}", this.linkedPartnerGroupInfoTokens[0], ":", this.linkedPartnerGroupInfoTokens[1]);
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x000256AE File Offset: 0x000238AE
		public bool Equals(LinkedPartnerGroupInformation other)
		{
			return other != null && this.LinkedPartnerOrganizationId.Equals(other.LinkedPartnerOrganizationId) && this.LinkedPartnerGroupId.Equals(other.LinkedPartnerGroupId);
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x000256DB File Offset: 0x000238DB
		public override bool Equals(object obj)
		{
			return this.Equals(obj as LinkedPartnerGroupInformation);
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x000256E9 File Offset: 0x000238E9
		public override int GetHashCode()
		{
			return this.LinkedPartnerGroupId.GetHashCode();
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x000256F8 File Offset: 0x000238F8
		private static string[] GetLinkedPartnerIdValues(string linkedPartnerIdAndOrganizationId)
		{
			string[] array = linkedPartnerIdAndOrganizationId.Split(new string[]
			{
				":"
			}, StringSplitOptions.None);
			if (array.Length == 2)
			{
				return array;
			}
			if (array.Length == 0)
			{
				return new string[]
				{
					string.Empty,
					string.Empty
				};
			}
			if (array.Length == 1)
			{
				if (linkedPartnerIdAndOrganizationId.StartsWith(":"))
				{
					return new string[]
					{
						string.Empty,
						array[0]
					};
				}
				if (linkedPartnerIdAndOrganizationId.EndsWith(":"))
				{
					return new string[]
					{
						array[0],
						string.Empty
					};
				}
			}
			throw new ArgumentException(DataStrings.LinkedPartnerGroupInformationInvalidParameter(linkedPartnerIdAndOrganizationId));
		}

		// Token: 0x04000745 RID: 1861
		private const string PartnerGroupIdSeperator = ":";

		// Token: 0x04000746 RID: 1862
		private const int InformationTokensCount = 2;

		// Token: 0x04000747 RID: 1863
		private const int PartnerGroupIdIndex = 0;

		// Token: 0x04000748 RID: 1864
		private const int PartnerOrganizationIdIndex = 1;

		// Token: 0x04000749 RID: 1865
		private string[] linkedPartnerGroupInfoTokens;
	}
}
