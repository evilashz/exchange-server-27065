using System;
using System.Data;
using System.IO;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000AA RID: 170
	public class FileReadSaver : FileSaver
	{
		// Token: 0x0600055B RID: 1371 RVA: 0x000143D0 File Offset: 0x000125D0
		public override void UpdateWorkUnits(DataRow row)
		{
			base.UpdateWorkUnits(row);
			string path = row[base.FilePathParameterName].ToString();
			this.workUnit.Text = Strings.FileReadTaskWorkUnitText;
			this.workUnit.Description = Strings.FileReadTaskWorkUnitDescription(path);
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00014424 File Offset: 0x00012624
		public override void Run(object interactionHandler, DataRow row, DataObjectStore store)
		{
			this.SavedResults.Clear();
			base.OnStart();
			string path = (string)row[base.FilePathParameterName];
			try
			{
				using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
				{
					using (BinaryReader binaryReader = new BinaryReader(fileStream))
					{
						byte[] fileData = binaryReader.ReadBytes((int)fileStream.Length);
						this.SavedResults.Add(new BinaryFileObject(Path.GetFileName(path), fileData));
						base.OnComplete(true, null);
					}
				}
			}
			catch (FileNotFoundException exception)
			{
				base.OnComplete(false, exception);
			}
			catch (UnauthorizedAccessException exception2)
			{
				base.OnComplete(false, exception2);
			}
			catch (DirectoryNotFoundException exception3)
			{
				base.OnComplete(false, exception3);
			}
			catch (PathTooLongException exception4)
			{
				base.OnComplete(false, exception4);
			}
		}
	}
}
