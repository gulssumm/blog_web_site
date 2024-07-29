pipeline {
  agent any
  stages {
    stage ('Initialize') {
      steps {
        echo 'GULSUM'
      }
    }
    stage ('for the jenkins branch') {
        when {
          branch "jenkins-*"
        }
        steps {
          bat '''
            cat README.md
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