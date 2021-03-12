using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics.Components.Monad;
using Microsoft.PowerShell.HostingTools;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001A4 RID: 420
	internal class MonadCommand : DbCommand, ICloneable
	{
		// Token: 0x06000F28 RID: 3880 RVA: 0x0002BC14 File Offset: 0x00029E14
		public MonadCommand()
		{
			ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "new MonadCommand()");
			this.CommandType = CommandType.StoredProcedure;
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x0002BC53 File Offset: 0x00029E53
		public MonadCommand(string cmdText) : this()
		{
			this.CommandText = cmdText;
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x0002BC62 File Offset: 0x00029E62
		public MonadCommand(string cmdText, MonadConnection connection) : this(cmdText)
		{
			this.Connection = connection;
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x0002BC74 File Offset: 0x00029E74
		private MonadCommand(MonadCommand from)
		{
			this.commandText = from.commandText;
			this.commandType = from.commandType;
			this.commandTimeout = from.commandTimeout;
			this.connection = from.connection;
			MonadParameterCollection parameters = this.Parameters;
			foreach (object obj in from.Parameters)
			{
				ICloneable cloneable = (ICloneable)obj;
				parameters.Add(cloneable.Clone());
			}
			this.ErrorReport += from.ErrorReport;
			this.WarningReport += from.WarningReport;
			this.StartExecution += from.StartExecution;
			this.EndExecution += from.EndExecution;
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000F2C RID: 3884 RVA: 0x0002BD5C File Offset: 0x00029F5C
		// (remove) Token: 0x06000F2D RID: 3885 RVA: 0x0002BD94 File Offset: 0x00029F94
		public event EventHandler<ProgressReportEventArgs> ProgressReport;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000F2E RID: 3886 RVA: 0x0002BDCC File Offset: 0x00029FCC
		// (remove) Token: 0x06000F2F RID: 3887 RVA: 0x0002BE04 File Offset: 0x0002A004
		public event EventHandler<ErrorReportEventArgs> ErrorReport;

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000F30 RID: 3888 RVA: 0x0002BE3C File Offset: 0x0002A03C
		// (remove) Token: 0x06000F31 RID: 3889 RVA: 0x0002BE74 File Offset: 0x0002A074
		public event EventHandler<WarningReportEventArgs> WarningReport;

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000F32 RID: 3890 RVA: 0x0002BEAC File Offset: 0x0002A0AC
		// (remove) Token: 0x06000F33 RID: 3891 RVA: 0x0002BEE4 File Offset: 0x0002A0E4
		public event EventHandler<StartExecutionEventArgs> StartExecution;

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06000F34 RID: 3892 RVA: 0x0002BF1C File Offset: 0x0002A11C
		// (remove) Token: 0x06000F35 RID: 3893 RVA: 0x0002BF54 File Offset: 0x0002A154
		public event EventHandler<RunGuidEventArgs> EndExecution;

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000F36 RID: 3894 RVA: 0x0002BF89 File Offset: 0x0002A189
		// (set) Token: 0x06000F37 RID: 3895 RVA: 0x0002BF91 File Offset: 0x0002A191
		[DefaultValue(null)]
		public new MonadConnection Connection
		{
			get
			{
				return this.connection;
			}
			set
			{
				ExTraceGlobals.IntegrationTracer.Information<string>((long)this.GetHashCode(), "MonadCommand.set_Connection({0})", (value == null) ? null : value.ConnectionString);
				if (this.pipelineProxy != null)
				{
					throw new InvalidOperationException("Cannot change the connection while a command is executing.");
				}
				this.connection = value;
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000F38 RID: 3896 RVA: 0x0002BFCF File Offset: 0x0002A1CF
		// (set) Token: 0x06000F39 RID: 3897 RVA: 0x0002BFD8 File Offset: 0x0002A1D8
		[DefaultValue(null)]
		public override string CommandText
		{
			get
			{
				return this.commandText;
			}
			set
			{
				ExTraceGlobals.IntegrationTracer.Information<string>((long)this.GetHashCode(), "MonadCommand.set_CommandText({0})", value);
				if (this.pipelineProxy != null)
				{
					throw new InvalidOperationException("Cannot change the command text while a command is executing.");
				}
				this.commandText = value;
				ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "<--MonadCommand.set_CommandText()");
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000F3A RID: 3898 RVA: 0x0002C02C File Offset: 0x0002A22C
		// (set) Token: 0x06000F3B RID: 3899 RVA: 0x0002C034 File Offset: 0x0002A234
		[DefaultValue(30)]
		public override int CommandTimeout
		{
			get
			{
				return this.commandTimeout;
			}
			set
			{
				ExTraceGlobals.IntegrationTracer.Information<int>((long)this.GetHashCode(), "-->MonadCommand.set_CommandTimeout({0})", value);
				if (value < 0)
				{
					throw new ArgumentException();
				}
				if (this.pipelineProxy != null)
				{
					throw new InvalidOperationException("Cannot change the command type while a command is executing.");
				}
				this.commandTimeout = value;
				ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "<--MonadCommand.set_CommandTimeout()");
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000F3C RID: 3900 RVA: 0x0002C092 File Offset: 0x0002A292
		// (set) Token: 0x06000F3D RID: 3901 RVA: 0x0002C09C File Offset: 0x0002A29C
		[DefaultValue(CommandType.StoredProcedure)]
		public override CommandType CommandType
		{
			get
			{
				return this.commandType;
			}
			set
			{
				ExTraceGlobals.IntegrationTracer.Information<CommandType>((long)this.GetHashCode(), "-->MonadCommand.set_CommandType({0})", value);
				if (value != CommandType.StoredProcedure && value != CommandType.Text)
				{
					throw new ArgumentException("Only StoredProcedure and Text modes are supported.");
				}
				if (value == CommandType.Text && this.Connection.IsPooled && !this.connection.IsRemote)
				{
					throw new ArgumentException("Scripts can only be executed on non-pooled connections.");
				}
				if (this.pipelineProxy != null)
				{
					throw new InvalidOperationException("Cannot change the command type while a command is executing.");
				}
				this.commandType = value;
				ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "<--MonadCommand.set_CommandType()");
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000F3E RID: 3902 RVA: 0x0002C12C File Offset: 0x0002A32C
		// (set) Token: 0x06000F3F RID: 3903 RVA: 0x0002C134 File Offset: 0x0002A334
		public override UpdateRowSource UpdatedRowSource
		{
			get
			{
				return this.updatedRowSource;
			}
			set
			{
				this.updatedRowSource = value;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000F40 RID: 3904 RVA: 0x0002C13D File Offset: 0x0002A33D
		// (set) Token: 0x06000F41 RID: 3905 RVA: 0x0002C140 File Offset: 0x0002A340
		[DefaultValue(false)]
		public override bool DesignTimeVisible
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000F42 RID: 3906 RVA: 0x0002C142 File Offset: 0x0002A342
		public new MonadParameterCollection Parameters
		{
			get
			{
				return this.parameterCollection;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000F43 RID: 3907 RVA: 0x0002C14A File Offset: 0x0002A34A
		internal Guid CommandGuid
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000F44 RID: 3908 RVA: 0x0002C152 File Offset: 0x0002A352
		// (set) Token: 0x06000F45 RID: 3909 RVA: 0x0002C15A File Offset: 0x0002A35A
		internal string PreservedObjectProperty
		{
			get
			{
				return this.preservedObjectProperty;
			}
			set
			{
				this.preservedObjectProperty = value;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000F46 RID: 3910 RVA: 0x0002C163 File Offset: 0x0002A363
		internal PowerShell ActivePipeline
		{
			get
			{
				if (this.pipelineProxy == null)
				{
					return null;
				}
				return this.pipelineProxy.PowerShell;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000F47 RID: 3911 RVA: 0x0002C17A File Offset: 0x0002A37A
		// (set) Token: 0x06000F48 RID: 3912 RVA: 0x0002C182 File Offset: 0x0002A382
		protected override DbConnection DbConnection
		{
			get
			{
				return this.Connection;
			}
			set
			{
				this.Connection = (MonadConnection)value;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000F49 RID: 3913 RVA: 0x0002C190 File Offset: 0x0002A390
		// (set) Token: 0x06000F4A RID: 3914 RVA: 0x0002C193 File Offset: 0x0002A393
		protected override DbTransaction DbTransaction
		{
			get
			{
				return null;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000F4B RID: 3915 RVA: 0x0002C19A File Offset: 0x0002A39A
		protected override DbParameterCollection DbParameterCollection
		{
			get
			{
				return this.Parameters;
			}
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x0002C1A4 File Offset: 0x0002A3A4
		public override int ExecuteNonQuery()
		{
			object[] array = this.Execute();
			int num = array.Length;
			if (num == 0)
			{
				num = -1;
			}
			return num;
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x0002C1C4 File Offset: 0x0002A3C4
		public override object ExecuteScalar()
		{
			object result = null;
			using (IDataReader dataReader = base.ExecuteReader())
			{
				if (0 < dataReader.FieldCount && dataReader.Read())
				{
					result = dataReader.GetValue(0);
				}
			}
			return result;
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x0002C210 File Offset: 0x0002A410
		public MonadAsyncResult BeginExecute(IEnumerable input)
		{
			IEnumerable enumerable;
			this.CreatePipeline(input, this.GetPipelineCommand(out enumerable), enumerable);
			return this.BeginExecute(enumerable != null);
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x0002C23C File Offset: 0x0002A43C
		public MonadAsyncResult BeginExecute(WorkUnit[] workUnits)
		{
			IEnumerable enumerable;
			this.CreatePipeline(workUnits, this.GetPipelineCommand(out enumerable));
			return this.BeginExecute(false);
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x0002C260 File Offset: 0x0002A460
		public object[] EndExecute(MonadAsyncResult asyncResult)
		{
			ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "-->MonadCommand.EndExecute()");
			if (asyncResult.RunningCommand != this)
			{
				throw new ArgumentException("Parameter does not correspond to this command.", "asyncResult");
			}
			List<object> list = null;
			try
			{
				Collection<PSObject> collection = this.pipelineProxy.EndInvoke(asyncResult);
				bool flag = false;
				if (this.Connection.IsRemote && collection.Count > 0 && collection[0] != null && collection[0].BaseObject != null && collection[0].BaseObject is PSCustomObject)
				{
					flag = MonadCommand.CanDeserialize(collection[0]);
				}
				list = new List<object>(collection.Count);
				ExTraceGlobals.IntegrationTracer.Information<int>((long)this.GetHashCode(), "\tPipeline contains {0} results", collection.Count);
				for (int i = 0; i < collection.Count; i++)
				{
					if (collection[i] == null)
					{
						ExTraceGlobals.VerboseTracer.Information<int>((long)this.GetHashCode(), "\tPipeline contains a null result at position {0}", i);
					}
					else
					{
						if (collection[i].BaseObject == null)
						{
							throw new InvalidOperationException("Pure PSObjects are not supported.");
						}
						if (!this.Connection.IsRemote)
						{
							list.Add(collection[i].BaseObject);
						}
						else if (flag)
						{
							list.Add(MonadCommand.Deserialize(collection[i]));
						}
						else
						{
							list.Add(collection[i]);
						}
					}
				}
			}
			finally
			{
				this.Connection.NotifyExecutionFinished();
				if (this.EndExecution != null)
				{
					this.EndExecution(this, new RunGuidEventArgs(this.guid));
				}
				this.Connection.CurrentCommand = null;
				this.pipelineProxy = null;
			}
			ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "<--MonadCommand.EndExecute()");
			return list.ToArray();
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x0002C434 File Offset: 0x0002A634
		public object[] Execute()
		{
			return this.Execute(null);
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x0002C440 File Offset: 0x0002A640
		public object[] Execute(object[] pipelineInput)
		{
			ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "-->MonadCommand.Execute(pipelineInput)");
			MonadAsyncResult asyncResult = this.BeginExecute(pipelineInput);
			object[] result = this.EndExecute(asyncResult);
			ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "<--MonadCommand.Execute()");
			return result;
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x0002C48C File Offset: 0x0002A68C
		public object[] Execute(WorkUnit[] workUnits)
		{
			ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "-->MonadCommand.Execute(workUnits)");
			MonadAsyncResult asyncResult = this.BeginExecute(workUnits);
			object[] result = this.EndExecute(asyncResult);
			ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "<--MonadCommand.Execute()");
			return result;
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x0002C4D8 File Offset: 0x0002A6D8
		public override void Cancel()
		{
			ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "-->MonadCommand.Cancel()");
			PowerShell activePipeline = this.ActivePipeline;
			if (activePipeline != null && activePipeline.InvocationStateInfo.State != PSInvocationState.NotStarted)
			{
				ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "\tStopping the pipeline.");
				activePipeline.Stop();
			}
			ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "<--MonadCommand.Cancel()");
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x0002C544 File Offset: 0x0002A744
		public override void Prepare()
		{
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x0002C548 File Offset: 0x0002A748
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(this.CommandText);
			foreach (object obj in this.Parameters)
			{
				MonadParameter monadParameter = (MonadParameter)obj;
				if (monadParameter.IsSwitch)
				{
					stringBuilder.Append(" -" + monadParameter.ParameterName);
				}
				else
				{
					string text = MonadCommand.FormatParameterValue(monadParameter.Value);
					if (!string.IsNullOrEmpty(text))
					{
						stringBuilder.Append(" -" + monadParameter.ParameterName + " " + text.ToString());
					}
					else
					{
						stringBuilder.Append(" -" + monadParameter.ParameterName + " ''");
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x0002C62C File Offset: 0x0002A82C
		public MonadCommand Clone()
		{
			return new MonadCommand(this);
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x0002C634 File Offset: 0x0002A834
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x0002C63C File Offset: 0x0002A83C
		internal static string FormatParameterValue(object value)
		{
			if (value != null && value is IList)
			{
				StringBuilder stringBuilder = new StringBuilder();
				if (value is byte[])
				{
					stringBuilder.AppendFormat("'{0}'", Strings.BinaryDataStakeHodler);
				}
				else
				{
					IList list = (IList)value;
					for (int i = 0; i < list.Count - 1; i++)
					{
						stringBuilder.Append(MonadCommand.FormatNonListParameterValue(list[i]) + ",");
					}
					if (list.Count > 0)
					{
						stringBuilder.Append(MonadCommand.FormatNonListParameterValue(list[list.Count - 1]));
					}
					else
					{
						stringBuilder.Append("@()");
					}
				}
				return stringBuilder.ToString();
			}
			return MonadCommand.FormatNonListParameterValue(value);
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x0002C6F8 File Offset: 0x0002A8F8
		internal static PSDataCollection<object> Serialize(IEnumerable collection)
		{
			PSDataCollection<object> psdataCollection = new PSDataCollection<object>();
			if (collection != null)
			{
				foreach (object obj in collection)
				{
					if (MonadCommand.CanSerialize(obj))
					{
						psdataCollection.Add(MonadCommand.Serialize(obj));
					}
					else if (obj is Enum)
					{
						psdataCollection.Add(obj.ToString());
					}
					else
					{
						psdataCollection.Add(obj);
					}
				}
			}
			psdataCollection.Complete();
			return psdataCollection;
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x0002C784 File Offset: 0x0002A984
		internal static PSObject Serialize(object obj)
		{
			if (obj is MapiFolderPath)
			{
				return new PSObject(obj.ToString());
			}
			PSObject psobject = new PSObject(obj);
			PSNoteProperty member = new PSNoteProperty("SerializationData", SerializationTypeConverter.GetSerializationData(psobject));
			psobject.Properties.Add(member);
			psobject.TypeNames.Insert(0, "Deserialized." + obj.GetType().FullName);
			return psobject;
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x0002C7EB File Offset: 0x0002A9EB
		internal static bool CanSerialize(object obj)
		{
			return SerializationTypeConverter.CanSerialize(obj);
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x0002C7F4 File Offset: 0x0002A9F4
		internal static Exception DeserializeException(Exception ex)
		{
			RemoteException ex2 = ex as RemoteException;
			if (ex2 != null && MonadCommand.CanDeserialize(ex2.SerializedRemoteException))
			{
				return (MonadCommand.Deserialize(ex2.SerializedRemoteException) as Exception) ?? ex;
			}
			return ex;
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x0002C830 File Offset: 0x0002AA30
		internal static bool CanDeserialize(PSObject psObject)
		{
			if (psObject == null || psObject.Members["SerializationData"] == null || psObject.Members["SerializationData"].Value == null)
			{
				return false;
			}
			Type destinationType = MonadCommand.ResolveType(psObject);
			return MonadCommand.TypeConverter.CanConvertFrom(psObject, destinationType);
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x0002C880 File Offset: 0x0002AA80
		internal static Type ResolveType(PSObject psObject)
		{
			if (psObject == null)
			{
				throw new ArgumentNullException("psObject");
			}
			lock (MonadCommand.syncInstance)
			{
				if (MonadCommand.typeDictionary.ContainsKey(psObject.TypeNames[0]))
				{
					return MonadCommand.typeDictionary[psObject.TypeNames[0]];
				}
			}
			string text = psObject.TypeNames[0].Substring("Deserialized.".Length);
			Type type = null;
			try
			{
				type = (Type)LanguagePrimitives.ConvertTo(text, typeof(Type));
			}
			catch (PSInvalidCastException)
			{
				type = MonadCommand.ResolvePSType(psObject, text);
				if (type == null)
				{
					throw;
				}
			}
			lock (MonadCommand.syncInstance)
			{
				if (!MonadCommand.typeDictionary.ContainsKey(psObject.TypeNames[0]))
				{
					MonadCommand.typeDictionary.Add(psObject.TypeNames[0], type);
				}
			}
			return type;
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x0002C9B4 File Offset: 0x0002ABB4
		internal static Type ResolvePSType(PSObject psObject, string typeName)
		{
			if (!typeName.StartsWith("System.Management.Automation", StringComparison.OrdinalIgnoreCase))
			{
				return null;
			}
			Assembly assembly = Assembly.GetAssembly(psObject.GetType());
			if (assembly != null)
			{
				return assembly.GetType(typeName);
			}
			return null;
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x0002C9F0 File Offset: 0x0002ABF0
		internal static object Deserialize(PSObject psObject)
		{
			if (psObject == null)
			{
				throw new ArgumentNullException("psObject");
			}
			if (psObject.Members["SerializationData"] != null && psObject.Members["SerializationData"].Value == null)
			{
				throw new ArgumentException("Cannot deserialize PSObject, SerializationData is missing.");
			}
			Type type = MonadCommand.ResolveType(psObject);
			if (psObject.Members["EMCMockEngineEnabled"] != null)
			{
				return MockObjectInformation.TranslateToMockObject(type, psObject);
			}
			return MonadCommand.TypeConverter.ConvertFrom(psObject, type, null, true);
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x0002CA6E File Offset: 0x0002AC6E
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.pipelineProxy != null)
			{
				this.pipelineProxy.Dispose();
				this.pipelineProxy = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x0002CA94 File Offset: 0x0002AC94
		protected override DbParameter CreateDbParameter()
		{
			return new MonadParameter();
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x0002CA9C File Offset: 0x0002AC9C
		protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
		{
			ExTraceGlobals.IntegrationTracer.Information<CommandBehavior>((long)this.GetHashCode(), "-->MonadCommand.ExecuteReader({0})", behavior);
			MonadAsyncResult asyncResult = this.BeginExecute(null);
			MonadDataReader result = new MonadDataReader(this, behavior, asyncResult, this.preservedObjectProperty);
			ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "<--MonadCommand.ExecuteReader()");
			return result;
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x0002CAF0 File Offset: 0x0002ACF0
		private static string FormatNonListParameterValue(object value)
		{
			string result = string.Empty;
			if (value == null)
			{
				result = MonadCommand.nullString;
			}
			else
			{
				Type type = value.GetType();
				if (type == typeof(bool))
				{
					result = (((bool)value) ? MonadCommand.trueString : MonadCommand.falseString);
				}
				else
				{
					result = string.Format("'{0}'", (value.ToString() != null) ? value.ToString().Replace("'", "''") : value.ToString());
				}
			}
			return result;
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x0002CB70 File Offset: 0x0002AD70
		private MonadAsyncResult BeginExecute(bool armedPipelineInputFromScript)
		{
			ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "-->MonadCommand.BeginExecute()");
			if (this.Connection == null)
			{
				throw new InvalidOperationException("Connection must be set before executing the command.");
			}
			this.Connection.NotifyExecutionStarting();
			this.Connection.CurrentCommand = this;
			IAsyncResult asyncResult = this.pipelineProxy.BeginInvoke(null, null);
			this.Connection.NotifyExecutionStarted();
			this.guid = Guid.NewGuid();
			if (this.StartExecution != null)
			{
				this.StartExecution(this, new StartExecutionEventArgs(this.CommandGuid, armedPipelineInputFromScript ? null : this.pipelineProxy.Input));
			}
			ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "<--MonadCommand.BeginExecute()");
			return (MonadAsyncResult)asyncResult;
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x0002CC30 File Offset: 0x0002AE30
		private PSCommand GetPipelineCommand(out IEnumerable pipelineInputFromScript)
		{
			ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "-->MonadCommand.GetPipelineCommand()");
			if (this.CommandText.Contains(" ") && this.CommandType == CommandType.StoredProcedure)
			{
				throw new InvalidOperationException("CommandType.StoredProcedure cannot be used to run scripts. Add any parameters to the Parameters collection.");
			}
			ExTraceGlobals.IntegrationTracer.Information<string>((long)this.GetHashCode(), "\tCreating command {0}", this.CommandText);
			bool flag = this.CommandType == CommandType.Text;
			PSCommand pscommand = null;
			pipelineInputFromScript = null;
			if (this.Connection != null && this.Connection.IsRemote && this.CommandType == CommandType.Text && MonadCommand.scriptRegex.IsMatch(this.CommandText))
			{
				this.ConvertScriptToStoreProcedure(this.CommandText, out pscommand, out pipelineInputFromScript);
			}
			else
			{
				pscommand = new PSCommand().AddCommand(new Command(this.CommandText, flag, !flag));
				foreach (object obj in this.Parameters)
				{
					MonadParameter monadParameter = (MonadParameter)obj;
					if (ParameterDirection.Input != monadParameter.Direction)
					{
						throw new InvalidOperationException("ParameterDirection.Input is the only supported parameter type.");
					}
					ExTraceGlobals.IntegrationTracer.Information<string, object>((long)this.GetHashCode(), "\tAdding parameter {0} = {1}", monadParameter.ParameterName, monadParameter.Value);
					if (this.connection.IsRemote && MonadCommand.CanSerialize(monadParameter.Value))
					{
						if (monadParameter.Value is ICollection)
						{
							pscommand.AddParameter(monadParameter.ParameterName, MonadCommand.Serialize(monadParameter.Value as IEnumerable));
						}
						else
						{
							pscommand.AddParameter(monadParameter.ParameterName, MonadCommand.Serialize(monadParameter.Value));
						}
					}
					else if (monadParameter.IsSwitch)
					{
						pscommand.AddParameter(monadParameter.ParameterName, true);
					}
					else
					{
						pscommand.AddParameter(monadParameter.ParameterName, monadParameter.Value);
					}
				}
			}
			ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "<--MonadCommand.GetPipelineCommand()");
			return pscommand;
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x0002CE30 File Offset: 0x0002B030
		private void CreatePipeline(WorkUnit[] workUnits, PSCommand commands)
		{
			ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "-->MonadCommand.CreatePipeline(workUnits)");
			if (this.pipelineProxy != null)
			{
				throw new InvalidOperationException("The command is already executing.");
			}
			if (this.Connection == null || (this.Connection.State & ConnectionState.Open) == ConnectionState.Closed)
			{
				throw new InvalidOperationException("The command requires an open connection.");
			}
			if (this.connection.IsRemote)
			{
				using (PSDataCollection<object> powerShellInput = WorkUnitBase.GetPowerShellInput<object>(workUnits))
				{
					this.pipelineProxy = new MonadPipelineProxy(this.Connection.RunspaceProxy, MonadCommand.Serialize(powerShellInput), commands, workUnits);
					goto IL_A7;
				}
			}
			this.pipelineProxy = new MonadPipelineProxy(this.Connection.RunspaceProxy, WorkUnitBase.GetPowerShellInput<object>(workUnits), commands, workUnits);
			IL_A7:
			this.InitializePipelineProxy();
			ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "<--MonadCommand.CreatePipeline(workUnits)");
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x0002CF10 File Offset: 0x0002B110
		private void CreatePipeline(IEnumerable pipelineInput, PSCommand commands, IEnumerable pipelineInputFromScript)
		{
			ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "-->MonadCommand.CreatePipeline(input)");
			if (this.pipelineProxy != null)
			{
				throw new InvalidOperationException("The command is already executing.");
			}
			if (this.Connection == null || (this.Connection.State & ConnectionState.Open) == ConnectionState.Closed)
			{
				throw new InvalidOperationException("The command requires an open connection.");
			}
			if (pipelineInputFromScript != null)
			{
				this.pipelineProxy = new MonadPipelineProxy(this.Connection.RunspaceProxy, pipelineInputFromScript, commands);
			}
			else if (pipelineInput != null)
			{
				if (this.connection.IsRemote)
				{
					this.pipelineProxy = new MonadPipelineProxy(this.Connection.RunspaceProxy, MonadCommand.Serialize(pipelineInput), commands);
				}
				else
				{
					this.pipelineProxy = new MonadPipelineProxy(this.Connection.RunspaceProxy, pipelineInput, commands);
				}
			}
			else
			{
				this.pipelineProxy = new MonadPipelineProxy(this.Connection.RunspaceProxy, commands);
			}
			this.InitializePipelineProxy();
			ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "<--MonadCommand.CreatePipeline(input)");
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x0002D004 File Offset: 0x0002B204
		private void InitializePipelineProxy()
		{
			if (this.pipelineProxy == null)
			{
				throw new InvalidOperationException("The PipelineProxy must be created before calling Initialize.");
			}
			this.pipelineProxy.InteractionHandler = this.Connection.InteractionHandler;
			this.pipelineProxy.Command = this;
			this.pipelineProxy.ErrorReport += this.ErrorReport;
			this.pipelineProxy.WarningReport += this.WarningReport;
			this.pipelineProxy.ProgressReport += this.ProgressReport;
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x0002D07C File Offset: 0x0002B27C
		private void ConvertScriptToStoreProcedure(string commandText, out PSCommand cmdlet, out IEnumerable pipelineInput)
		{
			Match match = MonadCommand.scriptRegex.Match(commandText);
			cmdlet = new PSCommand();
			CaptureCollection captures = match.Groups["pipelineInput"].Captures;
			object[] array = new object[captures.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.Unescape(captures[i].Value);
			}
			pipelineInput = array;
			this.RegisterCommand(match, cmdlet, "firstCmdletName", "firstParameterSet");
			if (!string.IsNullOrEmpty(match.Groups["secondCmdletName"].Value))
			{
				this.RegisterCommand(match, cmdlet, "secondCmdletName", "secondParameterSet");
			}
			if (!string.IsNullOrEmpty(match.Groups["thirdCmdletName"].Value))
			{
				this.RegisterCommand(match, cmdlet, "thirdCmdletName", "thirdParameterSet");
			}
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x0002D158 File Offset: 0x0002B358
		private PSCommand RegisterCommand(Match match, PSCommand cmdlet, string cmdletNameGroup, string parameterSetGroup)
		{
			cmdlet.AddCommand(new Command(match.Groups[cmdletNameGroup].Value, false, true));
			CaptureCollection captures = match.Groups[parameterSetGroup].Captures;
			string[] array = new string[captures.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = captures[i].Value;
			}
			int j = 0;
			while (j < array.Length)
			{
				if (j == 0 && !array[j].StartsWith("-"))
				{
					cmdlet.AddArgument(array[j]);
					j++;
				}
				else if (j + 1 == array.Length || (j + 1 < array.Length && array[j + 1].StartsWith("-")))
				{
					cmdlet.AddParameter(array[j].Substring(1), true);
					j++;
				}
				else
				{
					cmdlet.AddParameter(array[j].Substring(1), this.UnwrapValue(array[j + 1]));
					j += 2;
				}
			}
			return cmdlet;
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x0002D24C File Offset: 0x0002B44C
		private object UnwrapValue(string value)
		{
			Match match = MonadCommand.valueRegex.Match(value);
			CaptureCollection captures = match.Groups["value"].Captures;
			if (captures.Count == 1)
			{
				return this.Unescape(captures[0].Value);
			}
			IList<object> list = new List<object>();
			for (int i = 0; i < captures.Count; i++)
			{
				list.Add(this.Unescape(captures[i].Value));
			}
			return list;
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x0002D2C8 File Offset: 0x0002B4C8
		private object Unescape(string value)
		{
			string text = value.Replace("''", "'");
			if (string.Equals("$true", text, StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}
			if (string.Equals("$false", text, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			if (string.Equals("$null", text, StringComparison.OrdinalIgnoreCase))
			{
				return null;
			}
			return text;
		}

		// Token: 0x0400032D RID: 813
		private const int DefaultCommandTimeout = 30;

		// Token: 0x0400032E RID: 814
		private const CommandType DefaultCommandType = CommandType.StoredProcedure;

		// Token: 0x0400032F RID: 815
		internal static Regex scriptRegex = new Regex("^\\s*\t\t\t\t\t\t\t\t\t\t\t\t# match the begining of the line and any blank character\r\n                                    # Section for pipelineInput\r\n                                    (\r\n                                        (\r\n                                            '\t\t\t\t\t\t\t\t\t\t\t# any pipeline input object must begin with the character '\r\n                                                (?<pipelineInput>\t\t\t\t\t# put the value to the group pipelineInput, and we will do escape later\r\n                                                    (''|[^'])+\t\t\t\t\t\t# at least one character must be specific, and ' is used to escapte\r\n                                                )\r\n                                            '\t\t\t\t\t\t\t\t\t\t\t# any pipeline input object must end with the character '\r\n                                            \\s*\\,\\s*\t\t\t\t\t\t\t\t\t# if there are more than one pipelineinput, the character , is used to separate them\r\n                                        )*\r\n                                        (\r\n                                            '\t\t\t\t\t\t\t\t\t\t\t# any pipeline input object must begin with the character '\r\n                                                (?<pipelineInput>\t\t\t\t\t# put the value to the group pipelineInput, and we will do escape later\r\n                                                    (''|[^'])+\t\t\t\t\t\t# at least one character must be specific, and ' is used to escapte\r\n                                                )\r\n                                            '\t\t\t\t\t\t\t\t\t\t\t# any pipeline input object must end with the character '\r\n                                        )\t\t\t\t\t\t\t\t\t\t\t\t# Must have one and only one\r\n                                        \\s*\\|\\s*\t\t\t\t\t\t\t\t\t\t# | is the pipeline symbol\r\n                                    ){0,1}\r\n\r\n                                    # First Cmdlet Section\r\n                                    (?<firstCmdletName>\t\t\t\t\t\t# put the first cmdlet into the group firstCmdletName\r\n                                        [\\w,-]+\t\t\t\t\t\t\t\t\t\t# only digits, alphabets, _ and - are allowed for cmdlet name\r\n                                    )\t\t\t\t\t\t\t\t\t\t\t\t\t# match the end of the cmdlet name\r\n                                    \\s*\r\n\r\n                                    # [Optional] Argument\r\n                                    (?<firstParameterSet>\t\t\t\t\t\t# the first position is reserved for an argument, like \\;\r\n                                        '\t\t\t\t\t\t\t\t\t\t\t\t# A value can begin with ', which is used to escape!\r\n                                            (''|[^'])+\t\t\t\t\t\t\t\t# '' will be escaped to a single character '\r\n                                        '?\t\t\t\t\t\t\t\t\t\t\t\t# Another ' is needed to end a value\r\n                                        |\t\t\t\t\t\t\t\t\t\t\t\t# or\r\n                                            [\\w\\\\$]+\t\t\t\t\t\t\t\t\t# another format of a value\r\n                                    ){0,1}\r\n\r\n                                    (\\s+\t\t\t\t\t\t\t\t\t\t\t\t# match any blank character\r\n                                        (\r\n                                            (?<firstParameterSet>\t\t\t\t# put both parameterName and value to the group firstParameterSet\r\n                                                -\\w+\t\t\t\t\t\t\t\t\t# parameterName must begin with the character -\r\n                                            )\r\n\r\n                                            (?<firstParameterSet>\r\n                                                \\s+\r\n                                                (\r\n                                                    (\r\n                                                        '\t\t\t\t\t\t\t\t# A value can begin with ', which is used to escape!\r\n                                                            (''|[^'])+\t\t\t\t# '' will be escaped to a single character '\r\n                                                        '{1}?\t\t\t\t\t\t\t# Another ' is needed to end a value\r\n                                                        |\t\t\t\t\t\t\t\t# or\r\n                                                            [\\w\\\\$]+\t\t\t\t\t# another format of a value\r\n                                                    ),\t\t\t\t\t\t\t\t\t# we also support collection\r\n                                                )*\r\n                                                (\r\n                                                    '\t\t\t\t\t\t\t\t\t# A value can begin with ', which is used to escape!\r\n                                                        (''|[^'])+\t\t\t\t\t# '' will be escaped to a single character '\r\n                                                    '{1}?\t\t\t\t\t\t\t\t# Another ' is needed to end a value\r\n                                                    |\t\t\t\t\t\t\t\t\t# or\r\n                                                        [\\w\\\\$]+\t\t\t\t\t\t# another format of a value\r\n                                                )\t\t\t\t\t\t\t\t\t\t# the end of value\r\n                                            ){0,1}\r\n                                        )\r\n                                    )*\r\n\r\n                                    # Second Cmdlet Section\r\n                                    (\\s*\\|\\s*\\b\r\n                                    (?<secondCmdletName>\t\t\t\t\t# put the first cmdlet into the group firstCmdletName\r\n                                        [\\w,-]+\t\t\t\t\t\t\t\t\t\t# only digits, alphabets, _ and - are allowed for cmdlet name\r\n                                    )\t\t\t\t\t\t\t\t\t\t\t\t\t# match the end of the cmdlet name\r\n                                    \\s*\r\n\r\n                                    # [Optional] Argument\r\n                                    (?<secondParameterSet>\t\t\t\t\t# the first position is reserved for an argument, like \\;\r\n                                        '\t\t\t\t\t\t\t\t\t\t\t\t# A value can begin with ', which is used to escape!\r\n                                            (''|[^'])+\t\t\t\t\t\t\t\t# '' will be escaped to a single character '\r\n                                        '?\t\t\t\t\t\t\t\t\t\t\t\t# Another ' is needed to end a value\r\n                                        |\t\t\t\t\t\t\t\t\t\t\t\t# or\r\n                                            [\\w\\\\$]+\t\t\t\t\t\t\t\t\t# another format of a value\r\n                                    ){0,1}\r\n\r\n                                    (\\s+\t\t\t\t\t\t\t\t\t\t\t\t# match any blank character\r\n                                        (\r\n                                            (?<secondParameterSet>\t\t\t# put both parameterName and value to the group firstParameterSet\r\n                                                -\\w+\t\t\t\t\t\t\t\t\t# parameterName must begin with the character -\r\n                                            )\r\n\r\n                                            (?<secondParameterSet>\r\n                                                \\s+\r\n                                                (\r\n                                                    (\r\n                                                        '\t\t\t\t\t\t\t\t# A value can begin with ', which is used to escape!\r\n                                                            (''|[^'])+\t\t\t\t# '' will be escaped to a single character '\r\n                                                        '{1}?\t\t\t\t\t\t\t# Another ' is needed to end a value\r\n                                                        |\t\t\t\t\t\t\t\t# or\r\n                                                            [\\w\\\\$]+\t\t\t\t\t# another format of a value\r\n                                                    ),\t\t\t\t\t\t\t\t\t# we also support collection\r\n                                                )*\r\n                                                (\r\n                                                    '\t\t\t\t\t\t\t\t\t# A value can begin with ', which is used to escape!\r\n                                                        (''|[^'])+\t\t\t\t\t# '' will be escaped to a single character '\r\n                                                    '{1}?\t\t\t\t\t\t\t\t# Another ' is needed to end a value\r\n                                                    |\t\t\t\t\t\t\t\t\t# or\r\n                                                        [\\w\\\\$]+\t\t\t\t\t\t# another format of a value\r\n                                                )\t\t\t\t\t\t\t\t\t\t# the end of value\r\n                                            ){0,1}\r\n                                        )\r\n                                    )*\r\n                                    )?\r\n\r\n                                    # Third Cmdlet Section\r\n                                    (\\s*\\|\\s*\\b\r\n                                    (?<thirdCmdletName>\t\t\t\t\t\t# put the first cmdlet into the group firstCmdletName\r\n                                        [\\w,-]+\t\t\t\t\t\t\t\t\t\t# only digits, alphabets, _ and - are allowed for cmdlet name\r\n                                    )\t\t\t\t\t\t\t\t\t\t\t\t\t# match the end of the cmdlet name\r\n                                    \\s*\r\n\r\n                                    # [Optional] Argument\r\n                                    (?<thirdParameterSet>\t\t\t\t\t\t# the first position is reserved for an argument, like \\;\r\n                                        '\t\t\t\t\t\t\t\t\t\t\t\t# A value can begin with ', which is used to escape!\r\n                                            (''|[^'])+\t\t\t\t\t\t\t\t# '' will be escaped to a single character '\r\n                                        '?\t\t\t\t\t\t\t\t\t\t\t\t# Another ' is needed to end a value\r\n                                        |\t\t\t\t\t\t\t\t\t\t\t\t# or\r\n                                            [\\w\\\\$]+\t\t\t\t\t\t\t\t\t# another format of a value\r\n                                    ){0,1}\r\n\r\n                                    (\\s+\t\t\t\t\t\t\t\t\t\t\t\t# match any blank character\r\n                                        (\r\n                                            (?<thirdParameterSet>\t\t\t\t# put both parameterName and value to the group firstParameterSet\r\n                                                -\\w+\t\t\t\t\t\t\t\t\t# parameterName must begin with the character -\r\n                                            )\r\n\r\n                                            (?<thirdParameterSet>\r\n                                                \\s+\r\n                                                (\r\n                                                    (\r\n                                                        '\t\t\t\t\t\t\t\t# A value can begin with ', which is used to escape!\r\n                                                            (''|[^'])+\t\t\t\t# '' will be escaped to a single character '\r\n                                                        '{1}?\t\t\t\t\t\t\t# Another ' is needed to end a value\r\n                                                        |\t\t\t\t\t\t\t\t# or\r\n                                                            [\\w\\\\$]+\t\t\t\t\t# another format of a value\r\n                                                    ),\t\t\t\t\t\t\t\t\t# we also support collection\r\n                                                )*\r\n                                                (\r\n                                                    '\t\t\t\t\t\t\t\t\t# A value can begin with ', which is used to escape!\r\n                                                        (''|[^'])+\t\t\t\t\t# '' will be escaped to a single character '\r\n                                                    '{1}?\t\t\t\t\t\t\t\t# Another ' is needed to end a value\r\n                                                    |\t\t\t\t\t\t\t\t\t# or\r\n                                                        [\\w\\\\$]+\t\t\t\t\t\t# another format of a value\r\n                                                )\t\t\t\t\t\t\t\t\t\t# the end of value\r\n                                            ){0,1}\r\n                                        )\r\n                                    )*\r\n                                    )?\r\n                                  \\s*$", RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);

		// Token: 0x04000330 RID: 816
		private static string nullString = "$null";

		// Token: 0x04000331 RID: 817
		private static string trueString = "$true";

		// Token: 0x04000332 RID: 818
		private static string falseString = "$false";

		// Token: 0x04000333 RID: 819
		private static SerializationTypeConverter TypeConverter = new SerializationTypeConverter();

		// Token: 0x04000334 RID: 820
		private static object syncInstance = new object();

		// Token: 0x04000335 RID: 821
		private static Dictionary<string, Type> typeDictionary = new Dictionary<string, Type>();

		// Token: 0x04000336 RID: 822
		private static Regex valueRegex = new Regex("^\\s*\r\n                                        (\r\n                                            (\r\n                                                '\t\t\t\t\t\t\t\t\t\t\t\t# A value can begin with ', which is used to escape!\r\n                                                    (?<value>\r\n                                                        (''|[^'])+\t\t\t\t\t\t\t# '' will be escaped to a single character '\r\n                                                    )\r\n                                                '{1}?\t\t\t\t\t\t\t\t\t\t\t# Another ' is needed to end a value\r\n                                                |\t\t\t\t\t\t\t\t\t\t\t\t# or\r\n                                                (?<value>\r\n                                                    [\\w\\\\$]+\t\t\t\t\t\t\t\t\t# another format of a value\r\n                                                )\r\n                                            ),\t\t\t\t\t\t\t\t\t\t\t\t\t# we also support collection\r\n                                        )*\r\n                                        (\r\n                                            '\t\t\t\t\t\t\t\t\t\t\t\t\t# A value can begin with ', which is used to escape!\r\n                                                (?<value>\r\n                                                    (''|[^'])+\t\t\t    \t\t\t\t# '' will be escaped to a single character '\r\n                                                )\r\n                                            '{1}?\t\t\t\t\t\t\t\t\t\t\t\t# Another ' is needed to end a value\r\n                                            |\t\t\t\t\t\t\t\t\t\t\t\t\t# or\r\n                                            (?<value>\r\n                                                [\\w\\\\$]+\t\t\t\t\t\t\t\t\t\t# another format of a value\r\n                                            )\r\n                                        )\t\t\t\t\t\t\t\t\t\t\t\t\t\t# the end of value\r\n                                   $", RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);

		// Token: 0x04000337 RID: 823
		private Guid guid;

		// Token: 0x04000338 RID: 824
		private string commandText;

		// Token: 0x04000339 RID: 825
		private CommandType commandType;

		// Token: 0x0400033A RID: 826
		private int commandTimeout = 30;

		// Token: 0x0400033B RID: 827
		private UpdateRowSource updatedRowSource = UpdateRowSource.Both;

		// Token: 0x0400033C RID: 828
		private string preservedObjectProperty;

		// Token: 0x0400033D RID: 829
		private MonadPipelineProxy pipelineProxy;

		// Token: 0x0400033E RID: 830
		private MonadConnection connection;

		// Token: 0x0400033F RID: 831
		private MonadParameterCollection parameterCollection = new MonadParameterCollection();
	}
}
