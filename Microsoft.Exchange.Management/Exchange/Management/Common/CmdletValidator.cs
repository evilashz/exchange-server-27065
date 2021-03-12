using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;

namespace Microsoft.Exchange.Management.Common
{
	// Token: 0x020000E8 RID: 232
	internal class CmdletValidator
	{
		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x0001C9D0 File Offset: 0x0001ABD0
		// (set) Token: 0x060006E7 RID: 1767 RVA: 0x0001C9D8 File Offset: 0x0001ABD8
		public HashSet<string> AllowedCommands
		{
			get
			{
				return this.allowedCommands;
			}
			set
			{
				this.allowedCommands = value;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x0001C9E1 File Offset: 0x0001ABE1
		// (set) Token: 0x060006E9 RID: 1769 RVA: 0x0001C9E9 File Offset: 0x0001ABE9
		public Dictionary<string, HashSet<string>> RequiredParameters
		{
			get
			{
				return this.requiredParameters;
			}
			set
			{
				this.requiredParameters = value;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x0001C9F2 File Offset: 0x0001ABF2
		// (set) Token: 0x060006EB RID: 1771 RVA: 0x0001C9FA File Offset: 0x0001ABFA
		public Dictionary<string, HashSet<string>> NotAllowedParameters
		{
			get
			{
				return this.notAllowedParameters;
			}
			set
			{
				this.notAllowedParameters = value;
			}
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0001CA03 File Offset: 0x0001AC03
		public CmdletValidator(HashSet<string> allowedCommands, Dictionary<string, HashSet<string>> requiredParameters = null, Dictionary<string, HashSet<string>> notAllowedParameters = null)
		{
			this.AllowedCommands = allowedCommands;
			this.RequiredParameters = requiredParameters;
			this.NotAllowedParameters = notAllowedParameters;
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0001CA40 File Offset: 0x0001AC40
		public bool ValidateCmdlet(string cmdlet)
		{
			if (string.IsNullOrWhiteSpace(cmdlet))
			{
				return false;
			}
			Collection<PSParseError> collection2;
			Collection<PSToken> collection = PSParser.Tokenize(cmdlet, out collection2);
			if ((collection2 != null && collection2.Count > 0) || collection == null)
			{
				return false;
			}
			List<PSToken> list = (from token in collection
			where token.Type == PSTokenType.Command
			select token).ToList<PSToken>();
			if (list.Count != 1)
			{
				return false;
			}
			string content = list.First<PSToken>().Content;
			if (!this.AllowedCommands.Contains(content))
			{
				return false;
			}
			HashSet<string> hashSet = new HashSet<string>(from token in collection
			where token.Type == PSTokenType.CommandParameter
			select token into pstoken
			select pstoken.Content, StringComparer.OrdinalIgnoreCase);
			if (this.NotAllowedParameters != null && this.NotAllowedParameters.ContainsKey(content))
			{
				HashSet<string> hashSet2 = this.NotAllowedParameters[content];
				foreach (string item in hashSet)
				{
					if (hashSet2.Contains(item))
					{
						return false;
					}
				}
			}
			if (this.RequiredParameters != null && this.RequiredParameters.ContainsKey(content))
			{
				HashSet<string> hashSet3 = this.RequiredParameters[content];
				foreach (string item2 in hashSet3)
				{
					if (!hashSet.Contains(item2))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x04000342 RID: 834
		private HashSet<string> allowedCommands;

		// Token: 0x04000343 RID: 835
		private Dictionary<string, HashSet<string>> requiredParameters;

		// Token: 0x04000344 RID: 836
		private Dictionary<string, HashSet<string>> notAllowedParameters;
	}
}
