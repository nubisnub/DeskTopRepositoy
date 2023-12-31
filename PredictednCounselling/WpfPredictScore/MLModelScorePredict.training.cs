﻿﻿// This file was auto-generated by ML.NET Model Builder. 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers.FastTree;
using Microsoft.ML.Trainers;
using Microsoft.ML;

namespace WpfPredictScore
{
    public partial class MLModelScorePredict
    {
        /// <summary>
        /// Retrains model using the pipeline generated as part of the training process. For more information on how to load data, see aka.ms/loaddata.
        /// </summary>
        /// <param name="mlContext"></param>
        /// <param name="trainData"></param>
        /// <returns></returns>
        public static ITransformer RetrainPipeline(MLContext mlContext, IDataView trainData)
        {
            var pipeline = BuildPipeline(mlContext);
            var model = pipeline.Fit(trainData);

            return model;
        }

        /// <summary>
        /// build the pipeline that is used from model builder. Use this function to retrain model.
        /// </summary>
        /// <param name="mlContext"></param>
        /// <returns></returns>
        public static IEstimator<ITransformer> BuildPipeline(MLContext mlContext)
        {
            // Data process configuration with pipeline data transformations
            var pipeline = mlContext.Transforms.ReplaceMissingValues(new []{new InputOutputColumnPair(@"col0", @"col0"),new InputOutputColumnPair(@"col1", @"col1"),new InputOutputColumnPair(@"col2", @"col2"),new InputOutputColumnPair(@"col3", @"col3"),new InputOutputColumnPair(@"col4", @"col4"),new InputOutputColumnPair(@"col5", @"col5"),new InputOutputColumnPair(@"col6", @"col6"),new InputOutputColumnPair(@"col7", @"col7"),new InputOutputColumnPair(@"col8", @"col8"),new InputOutputColumnPair(@"col9", @"col9")})      
                                    .Append(mlContext.Transforms.Concatenate(@"Features", new []{@"col0",@"col1",@"col2",@"col3",@"col4",@"col5",@"col6",@"col7",@"col8",@"col9"}))      
                                    .Append(mlContext.Regression.Trainers.FastTree(new FastTreeRegressionTrainer.Options(){NumberOfLeaves=4,MinimumExampleCountPerLeaf=12,NumberOfTrees=4,MaximumBinCountPerFeature=57,FeatureFraction=0.99999999,LearningRate=0.999999776672986,LabelColumnName=@"col10",FeatureColumnName=@"Features"}));

            return pipeline;
        }
    }
}
