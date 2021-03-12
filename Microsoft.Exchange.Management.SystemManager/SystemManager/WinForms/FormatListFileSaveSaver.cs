using System;
using System.Data;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.MonadDataProvider;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000AC RID: 172
	public class FormatListFileSaveSaver : FileSaveSaver
	{
		// Token: 0x06000562 RID: 1378 RVA: 0x00014690 File Offset: 0x00012890
		static FormatListFileSaveSaver()
		{
			AppDomain.CurrentDomain.DomainUnload += FormatListFileSaveSaver.AppDomainUnloadEventHandler;
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x000146B7 File Offset: 0x000128B7
		private static void AppDomainUnloadEventHandler(object sender, EventArgs args)
		{
			FormatListFileSaveSaver.connection.Dispose();
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x000146CC File Offset: 0x000128CC
		public override void Run(object interactionHandler, DataRow row, DataObjectStore store)
		{
			base.OnStart();
			object value = row[base.FileDataParameterName];
			try
			{
				using (new OpenConnection(FormatListFileSaveSaver.connection))
				{
					object[] value2 = null;
					string variableName = "FormatEnumerationLimit";
					int num = -1;
					object variable = this.GetVariable(variableName, FormatListFileSaveSaver.connection);
					if (variable != null)
					{
						int.TryParse(variable.ToString(), out num);
					}
					this.SetVariable(variableName, -1, FormatListFileSaveSaver.connection);
					using (MonadCommand monadCommand = new MonadCommand("Format-List", FormatListFileSaveSaver.connection))
					{
						monadCommand.Parameters.Add(new MonadParameter("InputObject", value));
						value2 = monadCommand.Execute();
					}
					using (MonadCommand monadCommand2 = new MonadCommand("Out-File", FormatListFileSaveSaver.connection))
					{
						monadCommand2.Parameters.Add(new MonadParameter("InputObject", value2));
						monadCommand2.Parameters.Add(new MonadParameter("FilePath", row[base.FilePathParameterName]));
						monadCommand2.Execute();
					}
					if (num != -1)
					{
						this.SetVariable(variableName, num, FormatListFileSaveSaver.connection);
					}
					base.OnComplete(true, null);
				}
			}
			catch (CommandExecutionException exception)
			{
				base.OnComplete(false, exception);
			}
			catch (CmdletInvocationException exception2)
			{
				base.OnComplete(false, exception2);
			}
			catch (PipelineStoppedException exception3)
			{
				base.OnComplete(false, exception3);
			}
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x000148C0 File Offset: 0x00012AC0
		private void SetVariable(string variableName, object value, MonadConnection connection)
		{
			using (MonadCommand monadCommand = new MonadCommand("Set-Variable", connection))
			{
				monadCommand.Parameters.AddWithValue("Name", variableName);
				monadCommand.Parameters.AddWithValue("Value", value);
				monadCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00014920 File Offset: 0x00012B20
		private object GetVariable(string variableName, MonadConnection connection)
		{
			object result;
			using (MonadCommand monadCommand = new MonadCommand("Get-Variable", connection))
			{
				monadCommand.Parameters.AddWithValue("Name", variableName);
				object[] array = monadCommand.Execute();
				result = ((array.Length > 0) ? (array[0] as PSVariable).Value : null);
			}
			return result;
		}

		// Token: 0x040001C4 RID: 452
		private static MonadConnection connection = new MonadConnection("pooled=false");
	}
}
