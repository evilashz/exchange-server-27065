using System;
using System.IO;
using System.Management.Automation;
using System.Security;
using System.Xml;
using System.Xml.XPath;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200022D RID: 557
	[Cmdlet("Remove", "XmlNode")]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RemoveXmlNode : Task
	{
		// Token: 0x060012D9 RID: 4825 RVA: 0x00052942 File Offset: 0x00050B42
		public RemoveXmlNode()
		{
			TaskLogger.LogEnter();
			this.ExchangeInstallPath = ConfigurationContext.Setup.InstallPath;
			TaskLogger.LogExit();
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x060012DA RID: 4826 RVA: 0x0005295F File Offset: 0x00050B5F
		// (set) Token: 0x060012DB RID: 4827 RVA: 0x00052976 File Offset: 0x00050B76
		[Parameter(Mandatory = false)]
		public string ExchangeInstallPath
		{
			get
			{
				return (string)base.Fields["ExchangeInstallPath"];
			}
			set
			{
				base.Fields["ExchangeInstallPath"] = value;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x060012DC RID: 4828 RVA: 0x00052989 File Offset: 0x00050B89
		// (set) Token: 0x060012DD RID: 4829 RVA: 0x000529A0 File Offset: 0x00050BA0
		[Parameter(Mandatory = true)]
		public string XmlFileRelativePath
		{
			get
			{
				return (string)base.Fields["XmlFileRelativePath"];
			}
			set
			{
				base.Fields["XmlFileRelativePath"] = value;
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x060012DE RID: 4830 RVA: 0x000529B3 File Offset: 0x00050BB3
		// (set) Token: 0x060012DF RID: 4831 RVA: 0x000529CA File Offset: 0x00050BCA
		[Parameter(Mandatory = true)]
		public string XmlFileName
		{
			get
			{
				return (string)base.Fields["XmlFileName"];
			}
			set
			{
				base.Fields["XmlFileName"] = value;
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x060012E0 RID: 4832 RVA: 0x000529DD File Offset: 0x00050BDD
		// (set) Token: 0x060012E1 RID: 4833 RVA: 0x000529F4 File Offset: 0x00050BF4
		[Parameter(Mandatory = true)]
		public string XmlNodeNameToRemove
		{
			get
			{
				return (string)base.Fields["XmlNodeNameToRemove"];
			}
			set
			{
				base.Fields["XmlNodeNameToRemove"] = value;
			}
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x00052A08 File Offset: 0x00050C08
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				string text = Path.Combine(Path.Combine(this.ExchangeInstallPath, this.XmlFileRelativePath), this.XmlFileName);
				base.WriteVerbose(Strings.VerboseTaskParameters(this.ExchangeInstallPath, this.XmlFileRelativePath, text, this.XmlNodeNameToRemove));
				SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
				safeXmlDocument.Load(text);
				XmlNode xmlNode = safeXmlDocument.DocumentElement.SelectSingleNode(this.XmlNodeNameToRemove);
				if (xmlNode != null)
				{
					safeXmlDocument.DocumentElement.RemoveChild(xmlNode);
					safeXmlDocument.Save(text);
				}
			}
			catch (XmlException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidOperation, null);
			}
			catch (XPathException exception2)
			{
				base.WriteError(exception2, ErrorCategory.InvalidOperation, null);
			}
			catch (IOException exception3)
			{
				base.WriteError(exception3, ErrorCategory.InvalidOperation, null);
			}
			catch (UnauthorizedAccessException exception4)
			{
				base.WriteError(exception4, ErrorCategory.InvalidOperation, null);
			}
			catch (NotSupportedException exception5)
			{
				base.WriteError(exception5, ErrorCategory.InvalidOperation, null);
			}
			catch (SecurityException exception6)
			{
				base.WriteError(exception6, ErrorCategory.InvalidOperation, null);
			}
			catch (ArgumentException exception7)
			{
				base.WriteError(exception7, ErrorCategory.InvalidArgument, null);
			}
			TaskLogger.LogExit();
		}
	}
}
