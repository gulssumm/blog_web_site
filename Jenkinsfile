pipeline {
  agent any
  stages {
    stage ('Initialize') {
      steps {
        echo 'GULSUM'
      }
    }
    stage ('Build') {
      steps {
        sh 'dotnet build'
      }
    }
    stage ('Run') {
      steps {
        sh 'dotnet run'
      }
    }
    stage ('Migration') {
      steps {
        sh 'dotnet migrate'
      }
    }
  }
}