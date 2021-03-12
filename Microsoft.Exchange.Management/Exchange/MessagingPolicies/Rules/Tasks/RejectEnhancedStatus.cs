using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B66 RID: 2918
	[Serializable]
	public struct RejectEnhancedStatus : IComparable, ISerializable
	{
		// Token: 0x06006B1B RID: 27419 RVA: 0x001B7420 File Offset: 0x001B5620
		public RejectEnhancedStatus(string input)
		{
			this.value = null;
			if (this.IsValid(input))
			{
				this.value = input;
				return;
			}
			throw new ArgumentException(Strings.InvalidRejectEnhancedStatus, "RejectEnhancedStatus");
		}

		// Token: 0x06006B1C RID: 27420 RVA: 0x001B7450 File Offset: 0x001B5650
		private RejectEnhancedStatus(SerializationInfo info, StreamingContext context)
		{
			this.value = (string)info.GetValue("value", typeof(string));
			if (!this.IsValid(this.value))
			{
				throw new ArgumentException(Strings.InvalidRejectEnhancedStatus, "RejectEnhancedStatus");
			}
		}

		// Token: 0x1700213E RID: 8510
		// (get) Token: 0x06006B1D RID: 27421 RVA: 0x001B74A0 File Offset: 0x001B56A0
		public static RejectEnhancedStatus Empty
		{
			get
			{
				return default(RejectEnhancedStatus);
			}
		}

		// Token: 0x1700213F RID: 8511
		// (get) Token: 0x06006B1E RID: 27422 RVA: 0x001B74B6 File Offset: 0x001B56B6
		public string Value
		{
			get
			{
				if (this.IsValid(this.value))
				{
					return this.value;
				}
				throw new ArgumentException(Strings.InvalidRejectEnhancedStatus, "RejectEnhancedStatus");
			}
		}

		// Token: 0x06006B1F RID: 27423 RVA: 0x001B74E1 File Offset: 0x001B56E1
		public static RejectEnhancedStatus Parse(string s)
		{
			return new RejectEnhancedStatus(s);
		}

		// Token: 0x06006B20 RID: 27424 RVA: 0x001B74E9 File Offset: 0x001B56E9
		public static bool operator ==(RejectEnhancedStatus a, RejectEnhancedStatus b)
		{
			return a.Value == b.Value;
		}

		// Token: 0x06006B21 RID: 27425 RVA: 0x001B74FE File Offset: 0x001B56FE
		public static bool operator !=(RejectEnhancedStatus a, RejectEnhancedStatus b)
		{
			return a.Value != b.Value;
		}

		// Token: 0x06006B22 RID: 27426 RVA: 0x001B7513 File Offset: 0x001B5713
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("value", this.value);
		}

		// Token: 0x06006B23 RID: 27427 RVA: 0x001B7526 File Offset: 0x001B5726
		public override string ToString()
		{
			if (this.value == null)
			{
				return string.Empty;
			}
			return this.value.ToString();
		}

		// Token: 0x06006B24 RID: 27428 RVA: 0x001B7541 File Offset: 0x001B5741
		public override int GetHashCode()
		{
			if (this.value == null)
			{
				return string.Empty.GetHashCode();
			}
			return this.value.GetHashCode();
		}

		// Token: 0x06006B25 RID: 27429 RVA: 0x001B7561 File Offset: 0x001B5761
		public override bool Equals(object obj)
		{
			return obj is RejectEnhancedStatus && this.Equals((RejectEnhancedStatus)obj);
		}

		// Token: 0x06006B26 RID: 27430 RVA: 0x001B7579 File Offset: 0x001B5779
		public bool Equals(RejectEnhancedStatus obj)
		{
			return this.value == obj.Value;
		}

		// Token: 0x06006B27 RID: 27431 RVA: 0x001B7590 File Offset: 0x001B5790
		public int CompareTo(object obj)
		{
			if (!(obj is RejectEnhancedStatus))
			{
				throw new ArgumentException("Parameter is not of type RejectEnhancedStatus.");
			}
			return string.Compare(this.value, ((RejectEnhancedStatus)obj).Value, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06006B28 RID: 27432 RVA: 0x001B75CA File Offset: 0x001B57CA
		private bool IsValid(string input)
		{
			return input == null || (input.Length <= 7 && new Regex("^(5\\.7\\.1|5\\.7\\.[1-9][0-9]|5\\.7\\.[1-9][0-9][0-9])$", RegexOptions.Compiled).IsMatch(input) && EnhancedStatusCodeImpl.IsValid(input));
		}

		// Token: 0x04003703 RID: 14083
		public const int MaxLength = 7;

		// Token: 0x04003704 RID: 14084
		public const string AllowedCharacters = "[\\.0-9]";

		// Token: 0x04003705 RID: 14085
		public const string ValidatingExpression = "^(5\\.7\\.1|5\\.7\\.[1-9][0-9]|5\\.7\\.[1-9][0-9][0-9])$";

		// Token: 0x04003706 RID: 14086
		private string value;
	}
}
