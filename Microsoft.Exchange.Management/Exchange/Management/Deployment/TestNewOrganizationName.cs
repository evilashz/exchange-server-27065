using System;
using System.Management.Automation;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200026A RID: 618
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Test", "NewOrganizationName")]
	public sealed class TestNewOrganizationName : Task
	{
		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x060016E9 RID: 5865 RVA: 0x0006181A File Offset: 0x0005FA1A
		// (set) Token: 0x060016EA RID: 5866 RVA: 0x00061831 File Offset: 0x0005FA31
		[Parameter(Mandatory = true)]
		public string Value
		{
			get
			{
				return (string)base.Fields["Value"];
			}
			set
			{
				base.Fields["Value"] = value;
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x060016EB RID: 5867 RVA: 0x00061844 File Offset: 0x0005FA44
		private string ParameterName
		{
			get
			{
				return "Name";
			}
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x0006184C File Offset: 0x0005FA4C
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			if (char.IsWhiteSpace(this.Value[0]) || char.IsWhiteSpace(this.Value[this.Value.Length - 1]))
			{
				base.WriteError(new ArgumentException(Strings.ErrorLeadingTrailingWhitespaces(this.ParameterName, this.Value)), ErrorCategory.InvalidArgument, null);
			}
			if (!ADObjectNameHelper.ReservedADNameStringRegex.IsMatch(this.Value) && this.Value.Length > 64)
			{
				base.WriteError(new ArgumentException(Strings.ErrorNameValueStringTooLong(this.ParameterName, 64, this.Value.Length)), ErrorCategory.InvalidArgument, null);
			}
			int startIndex = -1;
			string str = CharacterConstraint.ConstructPattern(new char[]
			{
				'\0',
				'\n'
			}, false);
			if (ADObjectNameHelper.CheckIsUnicodeStringWellFormed(this.Value, out startIndex))
			{
				if (!ADObjectNameHelper.ReservedADNameStringRegex.IsMatch(this.Value) && !Regex.IsMatch(this.Value, "^" + str + "+$"))
				{
					base.WriteError(new ArgumentException(Strings.ErrorInvalidCharactersInParameterValue(this.ParameterName, this.Value, "'\0', '\n'")), ErrorCategory.InvalidArgument, null);
					return;
				}
			}
			else
			{
				base.WriteError(new ArgumentException(Strings.ErrorInvalidCharactersInParameterValue(this.ParameterName, this.Value, this.Value.Substring(startIndex, 1))), ErrorCategory.InvalidArgument, null);
			}
		}
	}
}
