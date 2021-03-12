using System;
using System.IO;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management
{
	// Token: 0x0200032E RID: 814
	[Cmdlet("Install", "FederatedDirectoryConfig")]
	public sealed class InstallFederatedDirectoryConfig : Task
	{
		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06001BBE RID: 7102 RVA: 0x0007B436 File Offset: 0x00079636
		// (set) Token: 0x06001BBF RID: 7103 RVA: 0x0007B43E File Offset: 0x0007963E
		[Parameter(Mandatory = false)]
		public string Filename { get; set; }

		// Token: 0x06001BC0 RID: 7104 RVA: 0x0007B448 File Offset: 0x00079648
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			string path = Path.Combine(ConfigurationContext.Setup.BinPath, "FederatedDirectory.config");
			File.Delete(path);
			string name = string.IsNullOrEmpty(this.Filename) ? "FederatedDirectory.config" : this.Filename;
			string value;
			using (Stream manifestResourceStream = base.GetType().Assembly.GetManifestResourceStream(name))
			{
				if (manifestResourceStream == null)
				{
					base.WriteError(new LocalizedException(Strings.ErrorObjectNotFound(name)), (ErrorCategory)1003, null);
				}
				using (StreamReader streamReader = new StreamReader(manifestResourceStream))
				{
					value = streamReader.ReadToEnd();
				}
			}
			using (StreamWriter streamWriter = new StreamWriter(path, false, Encoding.ASCII))
			{
				streamWriter.Write(value);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04000BE9 RID: 3049
		private const string DefaultFilename = "FederatedDirectory.config";
	}
}
