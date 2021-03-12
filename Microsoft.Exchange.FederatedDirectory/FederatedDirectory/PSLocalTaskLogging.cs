using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.FederatedDirectory
{
	// Token: 0x0200000E RID: 14
	internal static class PSLocalTaskLogging
	{
		// Token: 0x0200000F RID: 15
		internal struct CmdletStringBuilder
		{
			// Token: 0x06000090 RID: 144 RVA: 0x0000412B File Offset: 0x0000232B
			public void Append(string value)
			{
				this.InitializeIfNeeded();
				this.stringBuilder.Append(value);
			}

			// Token: 0x06000091 RID: 145 RVA: 0x00004140 File Offset: 0x00002340
			public void Append(string parameterName, string parameterValue)
			{
				this.InitializeIfNeeded();
				if (!string.IsNullOrEmpty(parameterValue))
				{
					this.stringBuilder.Append(string.Concat(new string[]
					{
						" -",
						parameterName,
						":'",
						parameterValue,
						"'"
					}));
				}
			}

			// Token: 0x06000092 RID: 146 RVA: 0x00004194 File Offset: 0x00002394
			public void Append(string parameterName, ADIdParameter parameterValue)
			{
				this.InitializeIfNeeded();
				if (parameterValue != null && !string.IsNullOrEmpty(parameterValue.RawIdentity))
				{
					this.stringBuilder.Append(string.Concat(new string[]
					{
						" -",
						parameterName,
						":'",
						parameterValue.RawIdentity,
						"'"
					}));
				}
			}

			// Token: 0x06000093 RID: 147 RVA: 0x000041F8 File Offset: 0x000023F8
			public void Append(string parameterName, Uri parameterValue)
			{
				this.InitializeIfNeeded();
				if (parameterValue != null)
				{
					this.stringBuilder.Append(string.Concat(new string[]
					{
						" -",
						parameterName,
						":'",
						parameterValue.ToString(),
						"'"
					}));
				}
			}

			// Token: 0x06000094 RID: 148 RVA: 0x00004254 File Offset: 0x00002454
			public void Append(string parameterName, RecipientIdParameter[] ids)
			{
				this.InitializeIfNeeded();
				if (ids != null && ids.Length > 0)
				{
					this.stringBuilder.Append(" -" + parameterName + ":");
					bool flag = true;
					foreach (RecipientIdParameter recipientIdParameter in ids)
					{
						if (flag)
						{
							flag = false;
						}
						else
						{
							this.stringBuilder.Append(",");
						}
						this.stringBuilder.Append("'" + recipientIdParameter.RawIdentity + "'");
					}
				}
			}

			// Token: 0x06000095 RID: 149 RVA: 0x000042DB File Offset: 0x000024DB
			public override string ToString()
			{
				this.InitializeIfNeeded();
				return this.stringBuilder.ToString();
			}

			// Token: 0x06000096 RID: 150 RVA: 0x000042EE File Offset: 0x000024EE
			private void InitializeIfNeeded()
			{
				if (this.stringBuilder == null)
				{
					this.stringBuilder = new StringBuilder(256);
				}
			}

			// Token: 0x04000055 RID: 85
			private StringBuilder stringBuilder;
		}

		// Token: 0x02000010 RID: 16
		internal sealed class NewGroupMailboxToString
		{
			// Token: 0x06000097 RID: 151 RVA: 0x00004308 File Offset: 0x00002508
			public NewGroupMailboxToString(NewGroupMailbox cmdlet)
			{
				this.cmdlet = cmdlet;
			}

			// Token: 0x06000098 RID: 152 RVA: 0x00004318 File Offset: 0x00002518
			public override string ToString()
			{
				PSLocalTaskLogging.CmdletStringBuilder cmdletStringBuilder = default(PSLocalTaskLogging.CmdletStringBuilder);
				cmdletStringBuilder.Append("New-GroupMailbox");
				cmdletStringBuilder.Append("ExecutingUser", this.cmdlet.ExecutingUser);
				cmdletStringBuilder.Append("Organization", this.cmdlet.Organization);
				cmdletStringBuilder.Append("ExternalDirectoryObjectId", this.cmdlet.ExternalDirectoryObjectId);
				cmdletStringBuilder.Append("Name", this.cmdlet.Name);
				cmdletStringBuilder.Append("ModernGroupType", this.cmdlet.ModernGroupType.ToString());
				cmdletStringBuilder.Append("Alias", this.cmdlet.Alias);
				cmdletStringBuilder.Append("Description", this.cmdlet.Description);
				cmdletStringBuilder.Append("Owners", this.cmdlet.Owners);
				cmdletStringBuilder.Append("Members", this.cmdlet.Members);
				return cmdletStringBuilder.ToString();
			}

			// Token: 0x04000056 RID: 86
			private readonly NewGroupMailbox cmdlet;
		}

		// Token: 0x02000011 RID: 17
		internal sealed class SetGroupMailboxToString
		{
			// Token: 0x06000099 RID: 153 RVA: 0x0000441F File Offset: 0x0000261F
			public SetGroupMailboxToString(SetGroupMailbox cmdlet)
			{
				this.cmdlet = cmdlet;
			}

			// Token: 0x0600009A RID: 154 RVA: 0x00004430 File Offset: 0x00002630
			public override string ToString()
			{
				PSLocalTaskLogging.CmdletStringBuilder cmdletStringBuilder = default(PSLocalTaskLogging.CmdletStringBuilder);
				cmdletStringBuilder.Append("Set-GroupMailbox");
				cmdletStringBuilder.Append("ExecutingUser", this.cmdlet.ExecutingUser);
				cmdletStringBuilder.Append("Identity", this.cmdlet.Identity);
				cmdletStringBuilder.Append("Name", this.cmdlet.Name);
				cmdletStringBuilder.Append("DisplayName", this.cmdlet.DisplayName);
				cmdletStringBuilder.Append("Description", this.cmdlet.Description);
				cmdletStringBuilder.Append("SharePointUrl", this.cmdlet.SharePointUrl);
				cmdletStringBuilder.Append("Owners", this.cmdlet.Owners);
				cmdletStringBuilder.Append("AddOwners", this.cmdlet.AddOwners);
				cmdletStringBuilder.Append("RemoveOwners", this.cmdlet.RemoveOwners);
				cmdletStringBuilder.Append("AddedMembers", this.cmdlet.AddedMembers);
				cmdletStringBuilder.Append("RemovedMembers", this.cmdlet.RemovedMembers);
				if (this.cmdlet.RequireSenderAuthenticationEnabledChanged)
				{
					cmdletStringBuilder.Append("RequireSenderAuthenticationEnabled", this.cmdlet.RequireSenderAuthenticationEnabled.ToString());
				}
				return cmdletStringBuilder.ToString();
			}

			// Token: 0x04000057 RID: 87
			private readonly SetGroupMailbox cmdlet;
		}

		// Token: 0x02000012 RID: 18
		internal sealed class RemoveGroupMailboxToString
		{
			// Token: 0x0600009B RID: 155 RVA: 0x00004587 File Offset: 0x00002787
			public RemoveGroupMailboxToString(RemoveGroupMailbox cmdlet)
			{
				this.cmdlet = cmdlet;
			}

			// Token: 0x0600009C RID: 156 RVA: 0x00004598 File Offset: 0x00002798
			public override string ToString()
			{
				PSLocalTaskLogging.CmdletStringBuilder cmdletStringBuilder = default(PSLocalTaskLogging.CmdletStringBuilder);
				cmdletStringBuilder.Append("Remove-GroupMailbox");
				cmdletStringBuilder.Append("ExecutingUser", this.cmdlet.ExecutingUser);
				cmdletStringBuilder.Append("Identity", this.cmdlet.Identity);
				return cmdletStringBuilder.ToString();
			}

			// Token: 0x04000058 RID: 88
			private readonly RemoveGroupMailbox cmdlet;
		}

		// Token: 0x02000013 RID: 19
		internal sealed class TaskOutputToString
		{
			// Token: 0x0600009D RID: 157 RVA: 0x000045F4 File Offset: 0x000027F4
			public TaskOutputToString(IList<PSLocalTaskIOData> container)
			{
				this.container = container;
			}

			// Token: 0x0600009E RID: 158 RVA: 0x00004604 File Offset: 0x00002804
			public override string ToString()
			{
				if (this.container != null)
				{
					StringBuilder stringBuilder = new StringBuilder(1000);
					stringBuilder.AppendLine("Output:");
					foreach (PSLocalTaskIOData pslocalTaskIOData in this.container)
					{
						stringBuilder.AppendLine(pslocalTaskIOData.ToString());
					}
					return stringBuilder.ToString();
				}
				return "No output";
			}

			// Token: 0x04000059 RID: 89
			private readonly IList<PSLocalTaskIOData> container;
		}
	}
}
