pipeline {
  agent any
  stages {
    stage ('Initialize') {
      steps {
        echo 'GULSUM'
      }
    stage ('Build') {
      steps {
        sh 'dotnet build'
      }
    }
    parallel {
      stage ('Run') {
        steps {
          sh 'dotnet run'
      }
      }
      stages ('Migration') {
        steps {
          sh 'dotnet migrate'
      }
    }
    }
  }
}
}