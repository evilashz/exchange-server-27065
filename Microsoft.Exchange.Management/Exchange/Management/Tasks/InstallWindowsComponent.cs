using System;
using System.IO;
using System.Management.Automation;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020001FD RID: 509
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("install", "windowscomponent")]
	public class InstallWindowsComponent : RunProcessBase
	{
		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001161 RID: 4449 RVA: 0x0004CA90 File Offset: 0x0004AC90
		// (set) Token: 0x06001162 RID: 4450 RVA: 0x0004CAA7 File Offset: 0x0004ACA7
		[Parameter(Mandatory = true)]
		public string ShortNameForRole
		{
			get
			{
				return (string)base.Fields["ShortNameForRole"];
			}
			set
			{
				base.Fields["ShortNameForRole"] = value;
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06001163 RID: 4451 RVA: 0x0004CABA File Offset: 0x0004ACBA
		// (set) Token: 0x06001164 RID: 4452 RVA: 0x0004CAD1 File Offset: 0x0004ACD1
		[Parameter(Mandatory = true)]
		public bool ADToolsNeeded
		{
			get
			{
				return (bool)base.Fields["ADToolsNeeded"];
			}
			set
			{
				base.Fields["ADToolsNeeded"] = value;
			}
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x0004CB00 File Offset: 0x0004AD00
		public InstallWindowsComponent()
		{
			base.ExeName = "ServerManagerCmd.exe";
			base.IgnoreExitCode = new int[]
			{
				1000,
				1001,
				1003,
				3010
			};
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x0004CB2C File Offset: 0x0004AD2C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			string text = null;
			try
			{
				string text2 = "Exchange-" + this.ShortNameForRole + ".xml";
				string text3 = Path.Combine(ConfigurationContext.Setup.BinPath, text2);
				string text4 = Path.Combine(ConfigurationContext.Setup.SetupLoggingPath, this.ShortNameForRole + "Prereqs.log");
				string text5 = text3;
				if (this.ADToolsNeeded)
				{
					SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
					safeXmlDocument.Load(text3);
					XmlNode documentElement = safeXmlDocument.DocumentElement;
					XmlElement xmlElement = safeXmlDocument.CreateElement("Feature", documentElement.NamespaceURI);
					xmlElement.SetAttribute("Id", "RSAT-ADDS");
					documentElement.AppendChild(xmlElement);
					string text6 = Path.Combine(ConfigurationContext.Setup.SetupLoggingPath, "temp" + text2);
					safeXmlDocument.Save(text6);
					text5 = text6;
					text = text6;
				}
				base.Args = string.Concat(new string[]
				{
					"-inputPath \"",
					text5,
					"\" -logPath \"",
					text4,
					"\""
				});
				base.InternalProcessRecord();
			}
			catch (IOException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidOperation, null);
			}
			catch (UnauthorizedAccessException exception2)
			{
				base.WriteError(exception2, ErrorCategory.SecurityError, null);
			}
			finally
			{
				if (text != null)
				{
					try
					{
						File.Delete(text);
					}
					catch
					{
					}
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x0004CCD4 File Offset: 0x0004AED4
		protected override void HandleProcessOutput(string outputString, string errorString)
		{
			Regex regex = new Regex("<\\d*/\\d*>\\s*");
			outputString = regex.Replace(outputString, "");
			base.HandleProcessOutput(outputString, errorString);
		}
	}
}
