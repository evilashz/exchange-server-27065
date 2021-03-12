using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Management.Automation;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.Monad;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001CF RID: 463
	internal class MonadDataReader : DbDataReader
	{
		// Token: 0x06001059 RID: 4185 RVA: 0x00031C3F File Offset: 0x0002FE3F
		internal MonadDataReader(MonadCommand command, CommandBehavior behavior, MonadAsyncResult asyncResult, string preservedObjectProperty) : this(command, behavior, asyncResult)
		{
			this.preservedObjectProperty = preservedObjectProperty;
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x00031C54 File Offset: 0x0002FE54
		internal MonadDataReader(MonadCommand command, CommandBehavior behavior, MonadAsyncResult asyncResult)
		{
			ExTraceGlobals.IntegrationTracer.Information<string, CommandBehavior>((long)this.GetHashCode(), "--> new MonadDataReader({0}, {1})", command.CommandText, behavior);
			this.command = command;
			this.commandBehavior = behavior;
			this.connection = command.Connection;
			this.isRemote = command.Connection.IsRemote;
			this.asyncResult = asyncResult;
			PSDataCollection<PSObject> output = asyncResult.Output;
			this.command.ActivePipeline.InvocationStateChanged += this.PipelineStateChanged;
			output.DataAdded += this.PipelineDataAdded;
			if (this.WaitOne(output))
			{
				ExTraceGlobals.VerboseTracer.Information((long)this.GetHashCode(), "\tFirst result ready.");
				PSObject psobject = output[0];
				PagedPositionInfo pagedPositionInfo = this.UnWrappPSObject(psobject) as PagedPositionInfo;
				if (pagedPositionInfo != null)
				{
					ExTraceGlobals.VerboseTracer.Information((long)this.GetHashCode(), "\tPagedPositionInfo object found.");
					output.RemoveAt(0);
					this.positionInfo = pagedPositionInfo;
					psobject = null;
					if (this.WaitOne(output))
					{
						psobject = output[0];
					}
				}
				if (psobject != null)
				{
					ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "\tFirst object returned, generating schema.");
					this.GenerateSchema(psobject);
					this.firstResult = this.UnWrappPSObject(psobject);
				}
			}
			ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "<-- new MonadDataReader()");
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x0600105B RID: 4187 RVA: 0x00031DB5 File Offset: 0x0002FFB5
		public override bool IsClosed
		{
			get
			{
				return this.isClosed;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x0600105C RID: 4188 RVA: 0x00031DC0 File Offset: 0x0002FFC0
		public override bool HasRows
		{
			get
			{
				ExTraceGlobals.VerboseTracer.Information((long)this.GetHashCode(), "-->MonadDataReader.HasRows");
				this.AssertReaderIsOpen();
				if (this.command.ActivePipeline == null)
				{
					return false;
				}
				bool flag = 0 != this.asyncResult.Output.Count;
				ExTraceGlobals.VerboseTracer.Information<bool>((long)this.GetHashCode(), "<--MonadDataReader.HasRows, {0}", flag);
				return flag;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x0600105D RID: 4189 RVA: 0x00031E29 File Offset: 0x00030029
		public override int FieldCount
		{
			get
			{
				if (!this.HasSchema)
				{
					return -1;
				}
				return this.properties.Count;
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x0600105E RID: 4190 RVA: 0x00031E40 File Offset: 0x00030040
		public override int RecordsAffected
		{
			get
			{
				int num = 1;
				if (this.recordsAffected != 0)
				{
					num = this.recordsAffected;
				}
				ExTraceGlobals.VerboseTracer.Information<int>((long)this.GetHashCode(), "MonadDataReader.RecordsAffected, {0}", num);
				return num;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x0600105F RID: 4191 RVA: 0x00031E76 File Offset: 0x00030076
		public override int Depth
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06001060 RID: 4192 RVA: 0x00031E79 File Offset: 0x00030079
		public PagedPositionInfo PositionInfo
		{
			get
			{
				return this.positionInfo;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06001061 RID: 4193 RVA: 0x00031E81 File Offset: 0x00030081
		public object FirstResult
		{
			get
			{
				return this.firstResult;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06001062 RID: 4194 RVA: 0x00031E89 File Offset: 0x00030089
		public object LastResult
		{
			get
			{
				return this.lastResult;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06001063 RID: 4195 RVA: 0x00031E91 File Offset: 0x00030091
		private bool HasSchema
		{
			get
			{
				return null != this.properties;
			}
		}

		// Token: 0x170002FD RID: 765
		public override object this[int ordinal]
		{
			get
			{
				return this.GetValue(ordinal);
			}
		}

		// Token: 0x170002FE RID: 766
		public override object this[string name]
		{
			get
			{
				ExTraceGlobals.DataTracer.Information<string>((long)this.GetHashCode(), "MonadDataReader.this[{0}]", name);
				int ordinal = this.GetOrdinal(name);
				if (ordinal >= 0)
				{
					return this.GetValue(ordinal);
				}
				return null;
			}
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x00031EE4 File Offset: 0x000300E4
		public override void Close()
		{
			ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "-->MonadDataReader.Close()");
			if (!this.IsClosed)
			{
				try
				{
					this.recordsAffected += this.command.EndExecute(this.asyncResult).Length;
					if (this.command.ActivePipeline != null)
					{
						this.command.ActivePipeline.InvocationStateChanged -= this.PipelineStateChanged;
						PSDataCollection<PSObject> output = this.asyncResult.Output;
						if (output != null)
						{
							output.DataAdded -= this.PipelineDataAdded;
						}
						this.UnblockWaitOne();
					}
				}
				finally
				{
					this.isClosed = true;
					if (this.commandBehavior == CommandBehavior.CloseConnection)
					{
						this.connection.Close();
					}
				}
			}
			ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "<--MonadDataReader.Close()");
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x00031FC8 File Offset: 0x000301C8
		public override bool Read()
		{
			ExTraceGlobals.VerboseTracer.Information((long)this.GetHashCode(), "MonadDataReader.Read()");
			this.AssertReaderIsOpen();
			if (this.command.ActivePipeline == null)
			{
				throw new InvalidOperationException("The command has been canceled.");
			}
			this.currentRecord = null;
			PSDataCollection<PSObject> output = this.asyncResult.Output;
			if (this.WaitOne(output))
			{
				PSObject psobject = output[0];
				output.RemoveAt(0);
				this.lastResult = this.UnWrappPSObject(psobject);
				if (this.isRemote && psobject.BaseObject is PSCustomObject && this.lastResult != psobject.BaseObject)
				{
					psobject = new PSObject(this.lastResult);
				}
				this.currentRecord = psobject;
				this.recordsAffected++;
			}
			ExTraceGlobals.VerboseTracer.Information<bool>((long)this.GetHashCode(), "<--MonadDataReader.Read(), {0}", null != this.currentRecord);
			return null != this.currentRecord;
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x000320B3 File Offset: 0x000302B3
		public override IEnumerator GetEnumerator()
		{
			return new DbEnumerator(this, this.commandBehavior == CommandBehavior.CloseConnection);
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x000320C5 File Offset: 0x000302C5
		public override bool NextResult()
		{
			this.AssertReaderIsOpen();
			return false;
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x000320D0 File Offset: 0x000302D0
		public override object GetValue(int ordinal)
		{
			ExTraceGlobals.DataTracer.Information<int>((long)this.GetHashCode(), "-->MonadDataReader.GetValue({0})", ordinal);
			this.AssertReaderIsOpen();
			object result;
			try
			{
				object obj = null;
				if (this.HasSchema && this.currentRecord != null)
				{
					if (this.useBaseObject)
					{
						if (ordinal == 0)
						{
							obj = this.currentRecord.BaseObject;
						}
					}
					else
					{
						COPropertyInfo copropertyInfo = this.properties[ordinal];
						if (copropertyInfo != null)
						{
							if (string.Equals(copropertyInfo.Name, this.preservedObjectProperty))
							{
								obj = this.currentRecord.BaseObject;
							}
							else
							{
								PSPropertyInfo pspropertyInfo = this.currentRecord.Properties[copropertyInfo.Name];
								if (pspropertyInfo != null)
								{
									obj = pspropertyInfo.Value;
									if (obj is PSObject)
									{
										object obj2 = this.UnWrappPSObject(obj as PSObject);
										if (obj2 is ArrayList)
										{
											obj = obj2;
										}
									}
									if (copropertyInfo.Type == typeof(string) && obj is IList)
									{
										obj = this.FormatListToString((IList)obj);
									}
									else if (copropertyInfo.Type == typeof(string) && obj != null && obj.GetType().IsEnum)
									{
										obj = LocalizedDescriptionAttribute.FromEnum(obj.GetType(), obj);
									}
									else if (copropertyInfo.Type == typeof(EnumObject))
									{
										obj = new EnumObject(obj as Enum);
									}
									else if (copropertyInfo.Type == typeof(string) && obj != null && obj.GetType() == typeof(bool))
									{
										bool flag = (bool)obj;
										if (flag)
										{
											obj = Strings.TrueString;
										}
										else
										{
											obj = Strings.FalseString;
										}
									}
									else if (obj != null && obj.GetType() == typeof(string))
									{
										if (copropertyInfo.Type.IsEnum)
										{
											obj = Enum.Parse(copropertyInfo.Type, obj as string);
										}
										else if (copropertyInfo.Type == typeof(Guid))
										{
											obj = new Guid(obj as string);
										}
									}
								}
							}
						}
					}
				}
				ExTraceGlobals.DataTracer.Information((long)this.GetHashCode(), "<--MonadDataReader.GetValue(), {0}", new object[]
				{
					obj
				});
				result = obj;
			}
			catch (GetValueException ex)
			{
				Exception innerException = ex.InnerException;
				while (innerException is TargetInvocationException)
				{
					innerException = innerException.InnerException;
				}
				if (innerException != null)
				{
					result = innerException.Message;
				}
				else
				{
					result = ex.Message;
				}
			}
			return result;
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x00032378 File Offset: 0x00030578
		public override int GetValues(object[] values)
		{
			this.AssertReaderIsOpen();
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (this.currentRecord == null)
			{
				throw new InvalidOperationException();
			}
			int num = Math.Min(values.Length, this.FieldCount);
			for (int i = 0; i < num; i++)
			{
				values[i] = this.GetValue(i);
			}
			return num;
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x000323D0 File Offset: 0x000305D0
		public override Type GetFieldType(int ordinal)
		{
			ExTraceGlobals.VerboseTracer.Information<int>((long)this.GetHashCode(), "-->MonadDataReader.GetFieldType({0})", ordinal);
			Type type = typeof(object);
			if (this.HasSchema)
			{
				COPropertyInfo copropertyInfo = this.properties[ordinal];
				if (copropertyInfo != null)
				{
					type = copropertyInfo.Type;
					if (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
					{
						type = Nullable.GetUnderlyingType(copropertyInfo.Type);
					}
				}
			}
			ExTraceGlobals.VerboseTracer.Information<Type>((long)this.GetHashCode(), "<--MonadDataReader.GetFieldType(), {0}", type);
			return type;
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x00032464 File Offset: 0x00030664
		public override string GetName(int ordinal)
		{
			ExTraceGlobals.VerboseTracer.Information<int>((long)this.GetHashCode(), "-->MonadDataReader.GetName({0})", ordinal);
			string text = null;
			if (this.HasSchema)
			{
				COPropertyInfo copropertyInfo = this.properties[ordinal];
				if (copropertyInfo != null)
				{
					text = copropertyInfo.Name;
				}
			}
			ExTraceGlobals.VerboseTracer.Information<string>((long)this.GetHashCode(), "<--MonadDataReader.GetName(), {0}", text);
			return text;
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x000324C4 File Offset: 0x000306C4
		public override int GetOrdinal(string name)
		{
			ExTraceGlobals.VerboseTracer.Information<string>((long)this.GetHashCode(), "-->MonadDataReader.GetOrdinal({0})", name);
			int num = -1;
			if (this.HasSchema)
			{
				num = this.properties.IndexOf(this.properties[name]);
			}
			ExTraceGlobals.VerboseTracer.Information<int>((long)this.GetHashCode(), "<--MonadDataReader.GetOrdinal(), {0}", num);
			return num;
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x00032524 File Offset: 0x00030724
		public override string GetDataTypeName(int ordinal)
		{
			ExTraceGlobals.VerboseTracer.Information<int>((long)this.GetHashCode(), "-->MonadDataReader.GetDataTypeName({0})", ordinal);
			string text = null;
			Type fieldType = this.GetFieldType(ordinal);
			if (null != fieldType)
			{
				text = fieldType.FullName;
			}
			ExTraceGlobals.VerboseTracer.Information<string>((long)this.GetHashCode(), "<--MonadDataReader.GetDataTypeName(), {0}", text);
			return text;
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x0003257C File Offset: 0x0003077C
		public void EnforceSchema(DataColumnCollection columnSet, DataColumnMappingCollection mappings)
		{
			ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "-->MonadDataReader.EnforceSchema()");
			if (columnSet == null || columnSet.Count == 0)
			{
				throw new ArgumentException("Parameter cannot be null or empty.", "columnSet");
			}
			this.useBaseObject = false;
			this.properties = new COPropertyInfoCollection();
			if (mappings != null)
			{
				for (int i = 0; i < mappings.Count; i++)
				{
					this.properties.Add(new COPropertyInfo(mappings[i].SourceColumn, columnSet[mappings[i].DataSetColumn].DataType));
					ExTraceGlobals.IntegrationTracer.Information<string>((long)this.GetHashCode(), "\tAdding mapped column {0}.", mappings[i].SourceColumn);
				}
			}
			else
			{
				for (int j = 0; j < columnSet.Count; j++)
				{
					if (string.IsNullOrEmpty(columnSet[j].Expression))
					{
						this.properties.Add(new COPropertyInfo(columnSet[j].ColumnName, columnSet[j].DataType));
					}
					ExTraceGlobals.IntegrationTracer.Information<string>((long)this.GetHashCode(), "\tAdding column {0}.", columnSet[j].ColumnName);
				}
			}
			ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "<--MonadDataReader.EnforceSchema()");
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x000326BB File Offset: 0x000308BB
		public override DataTable GetSchemaTable()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x000326C2 File Offset: 0x000308C2
		public override bool GetBoolean(int ordinal)
		{
			return (bool)this.GetValue(ordinal);
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x000326D0 File Offset: 0x000308D0
		public override byte GetByte(int ordinal)
		{
			return (byte)this.GetValue(ordinal);
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x000326DE File Offset: 0x000308DE
		public override long GetBytes(int ordinal, long fieldoffset, byte[] buffer, int bufferoffset, int length)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x000326E5 File Offset: 0x000308E5
		public override char GetChar(int ordinal)
		{
			return (char)this.GetValue(ordinal);
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x000326F3 File Offset: 0x000308F3
		public override long GetChars(int ordinal, long fieldoffset, char[] buffer, int bufferoffset, int length)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x000326FA File Offset: 0x000308FA
		public override DateTime GetDateTime(int ordinal)
		{
			return (DateTime)this.GetValue(ordinal);
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x00032708 File Offset: 0x00030908
		public override decimal GetDecimal(int ordinal)
		{
			return (decimal)this.GetValue(ordinal);
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x00032716 File Offset: 0x00030916
		public override double GetDouble(int ordinal)
		{
			return (double)this.GetValue(ordinal);
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x00032724 File Offset: 0x00030924
		public override float GetFloat(int ordinal)
		{
			return (float)this.GetValue(ordinal);
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x00032732 File Offset: 0x00030932
		public override Guid GetGuid(int ordinal)
		{
			return (Guid)this.GetValue(ordinal);
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x00032740 File Offset: 0x00030940
		public override short GetInt16(int ordinal)
		{
			return (short)this.GetValue(ordinal);
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x0003274E File Offset: 0x0003094E
		public override int GetInt32(int ordinal)
		{
			return (int)this.GetValue(ordinal);
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x0003275C File Offset: 0x0003095C
		public override long GetInt64(int ordinal)
		{
			return (long)this.GetValue(ordinal);
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x0003276A File Offset: 0x0003096A
		public override string GetString(int ordinal)
		{
			return (string)this.GetValue(ordinal);
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x00032778 File Offset: 0x00030978
		public override bool IsDBNull(int ordinal)
		{
			return false;
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x0003277B File Offset: 0x0003097B
		internal void PipelineDataAdded(object sender, DataAddedEventArgs e)
		{
			this.UnblockWaitOne();
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x00032783 File Offset: 0x00030983
		internal void PipelineStateChanged(object sender, PSInvocationStateChangedEventArgs e)
		{
			if (e.InvocationStateInfo.State == PSInvocationState.Failed || e.InvocationStateInfo.State == PSInvocationState.Stopped || e.InvocationStateInfo.State == PSInvocationState.Completed)
			{
				this.UnblockWaitOne();
			}
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x000327B5 File Offset: 0x000309B5
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x000327C7 File Offset: 0x000309C7
		private void AssertReaderIsOpen()
		{
			if (this.IsClosed)
			{
				throw new InvalidOperationException("Reader is already closed.");
			}
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x000327DC File Offset: 0x000309DC
		private void GenerateSchema(PSObject template)
		{
			ExTraceGlobals.IntegrationTracer.Information<PSObject>((long)this.GetHashCode(), "-->MonadDataReader.GenerateSchema({0})", template);
			this.properties = new COPropertyInfoCollection();
			IConfigurable configurable = this.UnWrappPSObject(template) as IConfigurable;
			if (configurable != null)
			{
				ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "\tTemplate is a IConfigurable");
				if (template.Properties["Identity"] != null)
				{
					Type typeFromHandle = typeof(string);
					this.properties.Add(new COPropertyInfo("Identity", typeFromHandle));
					ExTraceGlobals.IntegrationTracer.Information<string, Type>((long)this.GetHashCode(), "\tProperty added to schema: {0}, {1}", "Identity", typeFromHandle);
				}
				foreach (PSPropertyInfo pspropertyInfo in template.Properties)
				{
					if (!("Fields" == pspropertyInfo.Name) && !("Identity" == pspropertyInfo.Name))
					{
						Type type = Type.GetType(pspropertyInfo.TypeNameOfValue);
						if (null == type)
						{
							PropertyInfo property = configurable.GetType().GetProperty(pspropertyInfo.Name);
							if (!(null != property))
							{
								ExTraceGlobals.IntegrationTracer.Information<string>((long)this.GetHashCode(), "\tProperty skipped: {0}", pspropertyInfo.Name);
								continue;
							}
							type = property.PropertyType;
						}
						this.properties.Add(new COPropertyInfo(pspropertyInfo.Name, type));
						ExTraceGlobals.IntegrationTracer.Information<string>((long)this.GetHashCode(), "\tProperty added to schema: {0}", pspropertyInfo.Name);
					}
				}
				if (!string.IsNullOrEmpty(this.preservedObjectProperty))
				{
					this.properties.Add(new COPropertyInfo(this.preservedObjectProperty, configurable.GetType()));
					ExTraceGlobals.IntegrationTracer.Information<string>((long)this.GetHashCode(), "\tProperty added to schema: {0}", this.preservedObjectProperty);
				}
			}
			else
			{
				ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "\tTemplate is NOT a IConfigurable.");
				this.properties.Add(new COPropertyInfo("Value", this.UnWrappPSObject(template).GetType()));
				this.useBaseObject = true;
			}
			ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "<--MonadDataReader.GenerateSchema()");
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x00032A1C File Offset: 0x00030C1C
		private string FormatListToString(IList list)
		{
			string[] array = new string[list.Count];
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i] != null)
				{
					array[i] = list[i].ToString();
				}
			}
			return string.Join(CultureInfo.CurrentCulture.TextInfo.ListSeparator, array);
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x00032A73 File Offset: 0x00030C73
		private object UnWrappPSObject(PSObject element)
		{
			if (this.isRemote && element.BaseObject != null && element.BaseObject is PSCustomObject && MonadCommand.CanDeserialize(element))
			{
				return MonadCommand.Deserialize(element);
			}
			return element.BaseObject;
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x00032AA8 File Offset: 0x00030CA8
		private bool WaitOne(PSDataCollection<PSObject> output)
		{
			if (this.command.ActivePipeline == null)
			{
				return false;
			}
			lock (this.syncObject)
			{
				while (output.IsOpen && output.Count == 0 && (this.command.ActivePipeline.InvocationStateInfo.State == PSInvocationState.NotStarted || this.command.ActivePipeline.InvocationStateInfo.State == PSInvocationState.Running))
				{
					Monitor.Wait(this.syncObject);
				}
			}
			return output.Count > 0;
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x00032B48 File Offset: 0x00030D48
		private void UnblockWaitOne()
		{
			lock (this.syncObject)
			{
				Monitor.PulseAll(this.syncObject);
			}
		}

		// Token: 0x04000384 RID: 900
		private MonadCommand command;

		// Token: 0x04000385 RID: 901
		private MonadConnection connection;

		// Token: 0x04000386 RID: 902
		private int recordsAffected;

		// Token: 0x04000387 RID: 903
		private PSObject currentRecord;

		// Token: 0x04000388 RID: 904
		private COPropertyInfoCollection properties;

		// Token: 0x04000389 RID: 905
		private MonadAsyncResult asyncResult;

		// Token: 0x0400038A RID: 906
		private CommandBehavior commandBehavior;

		// Token: 0x0400038B RID: 907
		private bool isClosed;

		// Token: 0x0400038C RID: 908
		private bool useBaseObject;

		// Token: 0x0400038D RID: 909
		private bool isRemote;

		// Token: 0x0400038E RID: 910
		private string preservedObjectProperty = string.Empty;

		// Token: 0x0400038F RID: 911
		private object syncObject = new object();

		// Token: 0x04000390 RID: 912
		private PagedPositionInfo positionInfo;

		// Token: 0x04000391 RID: 913
		private object firstResult;

		// Token: 0x04000392 RID: 914
		private object lastResult;
	}
}
