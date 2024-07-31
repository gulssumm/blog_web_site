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
    stage ('for the jenkins branch') {
        when {
          branch "jenkins-*"
        }
        steps {
          bat '''
            type README.md
          '''
        }
    }
    stage ('for the PR') {
        when {
          branch 'PR-*'
        }
        steps {
          echo 'this only runs for the PRs'
        }
   }
  }
}