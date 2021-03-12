using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.ApplicationLogic.Performance;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.ActiveManager.Performance;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Performance;
using Microsoft.Exchange.OAB;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.OABGenerator
{
	// Token: 0x020001EB RID: 491
	internal sealed class AddressListFileGenerator
	{
		// Token: 0x06001335 RID: 4917 RVA: 0x0006F7C4 File Offset: 0x0006D9C4
		public AddressListFileGenerator(ADObjectId addressList, OABFile addressListFile, PropertyManager propertyManager, FileSet fileSet, GenerationStats stats, Action abortProcessingOnShutdown)
		{
			this.addressListFile = addressListFile;
			this.propertyManager = propertyManager;
			this.fileSet = fileSet;
			this.stats = stats;
			this.abortProcessingOnShutdown = abortProcessingOnShutdown;
			this.adAddressListEnumerator = ADAddressListEnumerator.Create(addressList, this.stats.OfflineAddressBook.OrganizationId, this.propertyManager.PropertyDefinitions, Globals.ADQueryPageSize, this.stats);
			this.firstPage = true;
			this.tempFiles = new List<FileStream>();
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06001336 RID: 4918 RVA: 0x0006F841 File Offset: 0x0006DA41
		public FileStream UncompressedSortedFlatFile
		{
			get
			{
				return this.uncompressedSortedFlatFile;
			}
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x0006F84C File Offset: 0x0006DA4C
		public AssistantTaskContext ProcessOnePageOfADResults(AssistantTaskContext assistantTaskContext)
		{
			OABGeneratorTaskContext oabgeneratorTaskContext = assistantTaskContext as OABGeneratorTaskContext;
			AssistantStep oabstep = new AssistantStep(this.ProduceSortedFlatFile);
			OABLogger.LogRecord(TraceType.FunctionTrace, "AddressListFileGenerator.ProcessOnePageOfADResults: start", new object[0]);
			try
			{
				using (new StopwatchPerformanceTracker("Total", this.stats))
				{
					using (new CpuPerformanceTracker("Total", this.stats))
					{
						using (new StopwatchPerformanceTracker("ProcessOnePageOfADResults", this.stats))
						{
							using (new CpuPerformanceTracker("ProcessOnePageOfADResults", this.stats))
							{
								if (this.adAddressListEnumerator.RetrievedAllData == null || !this.adAddressListEnumerator.RetrievedAllData.Value)
								{
									this.sortedEntries = this.adAddressListEnumerator.GetNextPageSorted();
									this.allResultsInSinglePage = (this.firstPage && this.adAddressListEnumerator.RetrievedAllData != null && this.adAddressListEnumerator.RetrievedAllData.Value);
									this.firstPage = false;
									if (!this.allResultsInSinglePage)
									{
										using (new StopwatchPerformanceTracker("ProcessOnePageOfADResults.ResolveLinks", this.stats))
										{
											using (new CpuPerformanceTracker("ProcessOnePageOfADResults.ResolveLinks", this.stats))
											{
												using (new ADPerformanceTracker("ProcessOnePageOfADResults.ResolveLinks", this.stats))
												{
													using (new ActiveManagerPerformanceTracker("ProcessOnePageOfADResults.ResolveLinks", this.stats))
													{
														this.propertyManager.ResolveLinks(this.sortedEntries);
													}
												}
											}
										}
										FileStream fileStream = this.fileSet.Create("TMP");
										this.tempFiles.Add(fileStream);
										this.stats.TotalNumberOfTempFiles++;
										using (new StopwatchPerformanceTracker("ProcessOnePageOfADResults.WriteTempFiles", this.stats))
										{
											using (new CpuPerformanceTracker("ProcessOnePageOfADResults.WriteTempFiles", this.stats))
											{
												using (IOCostStream iocostStream = new IOCostStream(new NoCloseStream(fileStream)))
												{
													using (new FileSystemPerformanceTracker("ProcessOnePageOfADResults.WriteTempFiles", iocostStream, this.stats))
													{
														using (BinaryWriter binaryWriter = new BinaryWriter(iocostStream))
														{
															foreach (ADRawEntry adrawEntry in this.sortedEntries)
															{
																this.abortProcessingOnShutdown();
																OABFileRecord oabfileRecord = this.CreateDetailsRecord(adrawEntry);
																binaryWriter.Write(((Guid)adrawEntry[ADObjectSchema.ExchangeObjectId]).ToByteArray());
																oabfileRecord.WriteTo(binaryWriter);
															}
															this.stats.IODuration += iocostStream.Writing;
														}
													}
												}
											}
										}
									}
								}
								if (this.adAddressListEnumerator.RetrievedAllData == null || !this.adAddressListEnumerator.RetrievedAllData.Value)
								{
									oabstep = new AssistantStep(this.ProcessOnePageOfADResults);
								}
							}
						}
					}
				}
			}
			finally
			{
				OABLogger.LogRecord(TraceType.FunctionTrace, "AddressListFileGenerator.ProcessOnePageOfADResults: finish", new object[0]);
				oabgeneratorTaskContext.OABStep = oabstep;
			}
			return OABGeneratorTaskContext.FromOABGeneratorTaskContext(oabgeneratorTaskContext);
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x0006FD1C File Offset: 0x0006DF1C
		public AssistantTaskContext ProduceSortedFlatFile(AssistantTaskContext assistantTaskContext)
		{
			OABGeneratorTaskContext oabgeneratorTaskContext = assistantTaskContext as OABGeneratorTaskContext;
			AssistantStep oabstep = oabgeneratorTaskContext.ReturnStep.Pop();
			OABLogger.LogRecord(TraceType.FunctionTrace, "AddressListFileGenerator.ProduceSortedFlatFile: start", new object[0]);
			try
			{
				using (new StopwatchPerformanceTracker("Total", this.stats))
				{
					using (new CpuPerformanceTracker("Total", this.stats))
					{
						using (new StopwatchPerformanceTracker("ProduceSortedFlatFile", this.stats))
						{
							using (new CpuPerformanceTracker("ProduceSortedFlatFile", this.stats))
							{
								this.stats.DomainControllersUsed.Add(this.adAddressListEnumerator.LastUsedDc);
								this.uncompressedSortedFlatFile = this.fileSet.Create("OAB");
								if (this.allResultsInSinglePage)
								{
									this.CreateFlatFileFromSinglePageOfResults(this.uncompressedSortedFlatFile, this.sortedEntries);
								}
								else
								{
									this.CreateFlatFileFromMultiplePagesOfResults(this.uncompressedSortedFlatFile, this.tempFiles);
								}
							}
						}
					}
				}
			}
			finally
			{
				OABLogger.LogRecord(TraceType.FunctionTrace, "AddressListFileGenerator.ProduceSortedFlatFile: finish", new object[0]);
				oabgeneratorTaskContext.OABStep = oabstep;
			}
			return OABGeneratorTaskContext.FromOABGeneratorTaskContext(oabgeneratorTaskContext);
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x0006FE94 File Offset: 0x0006E094
		private OABFileRecord CreateHeaderRecord()
		{
			OABPropertyValue[] array = new OABPropertyValue[]
			{
				new OABPropertyValue
				{
					PropTag = OABFilePropTags.OabName,
					Value = this.addressListFile.NameToUseInOABFile
				},
				new OABPropertyValue
				{
					PropTag = OABFilePropTags.OabDN,
					Value = Encoding.UTF8.GetBytes(this.addressListFile.DnToUseInOABFile)
				},
				new OABPropertyValue
				{
					PropTag = OABFilePropTags.OabSequence,
					Value = (int)(this.addressListFile.IsContinuationOfSequence ? (this.addressListFile.SequenceNumber + 1U) : this.addressListFile.SequenceNumber)
				},
				new OABPropertyValue
				{
					PropTag = OABFilePropTags.OabContainerGuid,
					Value = Encoding.UTF8.GetBytes(this.addressListFile.Guid.ToString())
				}
			};
			if (!string.IsNullOrWhiteSpace(this.addressListFile.DnOfTheHabRoot))
			{
				List<OABPropertyValue> list = new List<OABPropertyValue>(array.Length + 1);
				list.AddRange(array);
				list.Add(new OABPropertyValue
				{
					PropTag = OABFilePropTags.OabHABRootDepartmentLink,
					Value = Encoding.UTF8.GetBytes(this.addressListFile.DnOfTheHabRoot)
				});
				array = list.ToArray();
			}
			return new OABFileRecord
			{
				PropertyValues = array
			};
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x00070008 File Offset: 0x0006E208
		private OABFileRecord CreateDetailsRecord(ADRawEntry adRawEntry)
		{
			PropRow props = this.propertyManager.GetProps(adRawEntry);
			OABFileRecord oabfileRecord = new OABFileRecord
			{
				PropertyValues = new OABPropertyValue[props.Properties.Count]
			};
			for (int i = 0; i < props.Properties.Count; i++)
			{
				this.propertyManager.OABProperties[i].AddPropertyValue(adRawEntry, props, oabfileRecord, i);
			}
			return oabfileRecord;
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x00070070 File Offset: 0x0006E270
		private void CreateFlatFileFromSinglePageOfResults(Stream sortedFlatFileStream, ADRawEntry[] sortedEntries)
		{
			this.abortProcessingOnShutdown();
			using (IOCostStream iocostStream = new IOCostStream(new NoCloseStream(sortedFlatFileStream)))
			{
				using (new FileSystemPerformanceTracker("ProduceSortedFlatFile", iocostStream, this.stats))
				{
					using (BinaryWriter binaryWriter = new BinaryWriter(iocostStream))
					{
						using (CRCScratchPad crcscratchPad = new CRCScratchPad())
						{
							OABFileHeader oabfileHeader = new OABFileHeader();
							oabfileHeader.WriteTo(binaryWriter);
							uint defaultSeed = OABCRC.DefaultSeed;
							OABPropertyDescriptor[] array = new OABPropertyDescriptor[this.propertyManager.OABProperties.Length];
							for (int i = 0; i < array.Length; i++)
							{
								array[i] = this.propertyManager.OABProperties[i].PropertyDescriptor;
							}
							OABFileProperties data = new OABFileProperties
							{
								HeaderProperties = this.propertyManager.HeaderProperties,
								DetailProperties = array
							};
							crcscratchPad.ComputeCRCAndWrite(data, binaryWriter, ref defaultSeed);
							OABFileRecord data2 = this.CreateHeaderRecord();
							crcscratchPad.ComputeCRCAndWrite(data2, binaryWriter, ref defaultSeed);
							using (new StopwatchPerformanceTracker("ProduceSortedFlatFile", this.stats))
							{
								using (new CpuPerformanceTracker("ProduceSortedFlatFile", this.stats))
								{
									using (new ADPerformanceTracker("ProduceSortedFlatFile", this.stats))
									{
										using (new ActiveManagerPerformanceTracker("ProduceSortedFlatFile", this.stats))
										{
											this.propertyManager.ResolveLinks(sortedEntries);
										}
									}
								}
							}
							foreach (ADRawEntry adRawEntry in sortedEntries)
							{
								this.abortProcessingOnShutdown();
								OABFileRecord data3 = this.CreateDetailsRecord(adRawEntry);
								crcscratchPad.ComputeCRCAndWrite(data3, binaryWriter, ref defaultSeed);
							}
							iocostStream.Seek(0L, SeekOrigin.Begin);
							oabfileHeader.Version = 32;
							oabfileHeader.CRC = defaultSeed;
							oabfileHeader.RecordCount = sortedEntries.Length;
							oabfileHeader.WriteTo(binaryWriter);
							this.stats.IODuration += iocostStream.Writing;
							this.stats.TotalNumberOfRecords += sortedEntries.Length;
						}
					}
				}
			}
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x00070370 File Offset: 0x0006E570
		private void CreateFlatFileFromMultiplePagesOfResults(Stream sortedFlatFileStream, List<FileStream> tempFiles)
		{
			this.abortProcessingOnShutdown();
			using (IOCostStream iocostStream = new IOCostStream(new NoCloseStream(sortedFlatFileStream)))
			{
				using (new FileSystemPerformanceTracker("ProduceSortedFlatFile", iocostStream, this.stats))
				{
					using (BinaryWriter binaryWriter = new BinaryWriter(iocostStream))
					{
						using (CRCScratchPad crcscratchPad = new CRCScratchPad())
						{
							OABFileHeader oabfileHeader = new OABFileHeader();
							oabfileHeader.WriteTo(binaryWriter);
							uint num = OABCRC.DefaultSeed;
							OABPropertyDescriptor[] array = new OABPropertyDescriptor[this.propertyManager.OABProperties.Length];
							for (int i = 0; i < array.Length; i++)
							{
								array[i] = this.propertyManager.OABProperties[i].PropertyDescriptor;
							}
							OABFileProperties data = new OABFileProperties
							{
								HeaderProperties = this.propertyManager.HeaderProperties,
								DetailProperties = array
							};
							crcscratchPad.ComputeCRCAndWrite(data, binaryWriter, ref num);
							OABFileRecord data2 = this.CreateHeaderRecord();
							crcscratchPad.ComputeCRCAndWrite(data2, binaryWriter, ref num);
							int num2 = 0;
							using (TempFileReaderCollection tempFileReaderCollection = new TempFileReaderCollection())
							{
								tempFileReaderCollection.Initialize(this.stats, tempFiles);
								for (;;)
								{
									this.abortProcessingOnShutdown();
									byte[] nextRecord = tempFileReaderCollection.GetNextRecord();
									if (nextRecord == null)
									{
										break;
									}
									num = OABCRC.ComputeCRC(num, nextRecord, 0, nextRecord.Length);
									binaryWriter.Write(nextRecord);
									num2++;
								}
							}
							iocostStream.Seek(0L, SeekOrigin.Begin);
							oabfileHeader.Version = 32;
							oabfileHeader.CRC = num;
							oabfileHeader.RecordCount = num2;
							oabfileHeader.WriteTo(binaryWriter);
							this.stats.IODuration += iocostStream.Writing;
							this.stats.TotalNumberOfRecords += num2;
						}
					}
				}
			}
		}

		// Token: 0x04000BA6 RID: 2982
		private readonly Action abortProcessingOnShutdown;

		// Token: 0x04000BA7 RID: 2983
		private readonly OABFile addressListFile;

		// Token: 0x04000BA8 RID: 2984
		private readonly PropertyManager propertyManager;

		// Token: 0x04000BA9 RID: 2985
		private readonly FileSet fileSet;

		// Token: 0x04000BAA RID: 2986
		private readonly GenerationStats stats;

		// Token: 0x04000BAB RID: 2987
		private ADAddressListEnumerator adAddressListEnumerator;

		// Token: 0x04000BAC RID: 2988
		private ADRawEntry[] sortedEntries;

		// Token: 0x04000BAD RID: 2989
		private List<FileStream> tempFiles;

		// Token: 0x04000BAE RID: 2990
		private bool firstPage;

		// Token: 0x04000BAF RID: 2991
		private bool allResultsInSinglePage;

		// Token: 0x04000BB0 RID: 2992
		private FileStream uncompressedSortedFlatFile;
	}
}
