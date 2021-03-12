using System;
using System.Data;
using Microsoft.Exchange.Configuration.MonadDataProvider;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000022 RID: 34
	internal class MonadProvider : IMonadDataProvider
	{
		// Token: 0x0600005F RID: 95 RVA: 0x00002CCD File Offset: 0x00000ECD
		internal MonadProvider()
		{
			this.monadConnection = new MonadConnection("pooled=false");
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002CE8 File Offset: 0x00000EE8
		public object[] ExecuteCommand(string command)
		{
			if (command == string.Empty)
			{
				throw new ArgumentNullException("command");
			}
			object[] result = null;
			lock (this.monadConnection)
			{
				try
				{
					this.monadConnection.Open();
					using (MonadCommand monadCommand = new MonadCommand(command, this.monadConnection))
					{
						monadCommand.CommandType = CommandType.Text;
						result = monadCommand.Execute();
					}
				}
				finally
				{
					if (this.monadConnection.State == ConnectionState.Open)
					{
						this.monadConnection.Close();
					}
				}
			}
			return result;
		}

		// Token: 0x04000073 RID: 115
		private MonadConnection monadConnection;
	}
}
