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
        bat run.cmd
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