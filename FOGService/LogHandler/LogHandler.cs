﻿
using System;
using System.IO;
using System.Collections.Generic;

namespace FOG
{
	/// <summary>
	/// Handle all interaction with the log file
	/// </summary>
	public static class LogHandler
	{
		//Define variables
		private static String filePath = @"\fog.log";
		private static long maxLogSizeDefault = 502400;
		private static long maxLogSize = maxLogSizeDefault;
		private const String LOG_NAME = "LogHandler";
		private static Boolean console = false;
		private const int HEADER_LENGTH=64;

		public static void setFilePath(String fPath) { filePath = fPath; }		
		public static String getFilePath() { return filePath; }
		public static void setMaxLogSize(long mLogSize) { maxLogSize = mLogSize; }	
		public static long getMaxLogSize() { return maxLogSize; }
		public static void defaultMaxLogSize() { maxLogSize = maxLogSizeDefault; }
		public static void setConsoleMode(Boolean con) {  console = con; }
		
		//Log a message
		public static void log(String moduleName, String message) {
			writeLine(" " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + 
			          " " + moduleName + " " + message);
		}
		
		//Make a new line in the log file
		public static void newLine() {
			writeLine("");
		}
		
		//Make a divider in the log file
		public static void divider() {
			header("");
		}
		
		public static void header(String text) {
			double headerSize = (HEADER_LENGTH-text.Length)/2;
			
			for(int i=0; i<(int)Math.Ceiling(headerSize); i++) {
				write("-");
			}
			write(text);
			for(int i=0; i<((int)Math.Floor(headerSize)); i++) {
				write("-");
			}	
			newLine();
			
		}
		
		public static void paddedHeader(String text) {
			divider();
			header(text);
			divider();
		}
		
		//Write a string on the current line, it is prefered that log is used instead for formatting purposes
		public static void write(String text) {
			if(console) {
				if(text.ToUpper().Contains("ERROR"))
					Console.BackgroundColor = ConsoleColor.Red;
				Console.Write(text);
				Console.BackgroundColor = ConsoleColor.Black;
			} else {
				StreamWriter logWriter;
				FileInfo logFile = new FileInfo(getFilePath());
	
				//Delete the log file if it excedes the max log size
				if (logFile.Exists && logFile.Length > maxLogSize)
					cleanLog(logFile);
				
				try {
					//Write message to log file
					logWriter = new StreamWriter(getFilePath(), true);
					logWriter.Write(text);
					logWriter.Close();
				} catch {
					//If logging fails then nothing can really be done to silently notify the user
				} 		
			}			
		}
		
		//Write a string to a line, other classes should not call this function directly for formatting purposes
		public static void writeLine(String line) {
			write(line + "\n");
		}
		
		//Delete the log file and create a new one
		private static void cleanLog(FileInfo logFile) {
			try {
				logFile.Delete();
			} catch(Exception ex) {
				log(LOG_NAME, "Failed to delete log file: " + ex.Message);
			}
		}
	}
}