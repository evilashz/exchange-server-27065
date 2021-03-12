using System;
using System.Data;
using System.IO;
using System.Security;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000AB RID: 171
	public class FileSaveSaver : FileSaver
	{
		// Token: 0x0600055D RID: 1373 RVA: 0x0001452C File Offset: 0x0001272C
		public FileSaveSaver()
		{
			this.FileDataParameterName = "FileData";
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x0001453F File Offset: 0x0001273F
		// (set) Token: 0x0600055F RID: 1375 RVA: 0x00014547 File Offset: 0x00012747
		[DDIDataColumnExist]
		public string FileDataParameterName { get; set; }

		// Token: 0x06000560 RID: 1376 RVA: 0x00014550 File Offset: 0x00012750
		public override void UpdateWorkUnits(DataRow row)
		{
			base.UpdateWorkUnits(row);
			string path = row[base.FilePathParameterName].ToString();
			this.workUnit.Text = Strings.FileSaveTaskWorkUnitText;
			this.workUnit.Description = Strings.FileSaveTaskWorkUnitDescription(path);
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x000145A4 File Offset: 0x000127A4
		public override void Run(object interactionHandler, DataRow row, DataObjectStore store)
		{
			base.OnStart();
			string path = (string)row[base.FilePathParameterName];
			try
			{
				using (FileStream fileStream = new FileStream(path, FileMode.Create))
				{
					byte[] array = (byte[])row[this.FileDataParameterName];
					fileStream.Write(array, 0, array.Length);
					base.OnComplete(true, null);
				}
			}
			catch (UnauthorizedAccessException exception)
			{
				base.OnComplete(false, exception);
			}
			catch (DirectoryNotFoundException exception2)
			{
				base.OnComplete(false, exception2);
			}
			catch (PathTooLongException exception3)
			{
				base.OnComplete(false, exception3);
			}
			catch (SecurityException exception4)
			{
				base.OnComplete(false, exception4);
			}
			catch (IOException exception5)
			{
				base.OnComplete(false, exception5);
			}
		}
	}
}
